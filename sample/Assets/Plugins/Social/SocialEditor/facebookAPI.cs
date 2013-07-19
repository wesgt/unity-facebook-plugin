using System;
using UnityEngine;
using LitJson;

public class facebookAPI :socalAPI
{
    public facebookAPI (string appKey, string appSecret, string redirectURL,string scope)
    : base(appKey, appSecret, "facebook", redirectURL, scope)
    {

    }

    public class errorCode{
        public string message{
            get{
                return m_message;
            }
            set{
                m_message = value;
            }
        }
        public string type{
            get{
                return m_type;
            }
            set{
                m_type = value;
            }
        }
        public int code{
            get{
                return m_code;
            }
            set{
                m_code = value;
            }
        }

        string m_message;
        string m_type;
        int m_code;
    }

    public class Range{
        public int min;
        public int max;
    }

    public class Cover{
        public string id;
        public string source;
        public int offset_y;

    }

    public class Currency{
        string user_currency;
        int currency_exchange;
        float currency_exchange_inverse;
        int usd_exchange;
        int usd_exchange_inverse;
        int currency_offset;
    }

    public class Device{
        public string os;
        public string hardware;
    }

    public class Education{
        public NameInfo school;
        public string type;
    }

    public class PaymentPricePoint{
        public int user_price;
        public int credits;
        public string local_currency;
    }

    public class NameInfo{
        public string name;
        public string id;
    }

    public class SecuritySettings{
        public SecureBrowsing secure_browsing;
    }
    public class SecureBrowsing{
        public bool enabled;
    }

    public class VideoUploadLimits{
        public int length;
        public int size;
    }

    public class Work{
        public NameInfo employer;
        public NameInfo location;
        public string position;
        public string start_date;
        public string end_date;
    }

    public class User{
        public string id;
        public string name;
        public string first_name;
        public string middle_name;
        public string last_name;
        public string gender;
        public string locale;
        public NameInfo[] languages;
        public string link;
        public string username;
        public Range age_range;
        public string third_party_id;
        public string installed;
        public int timezone;
        public string updated_time;
        public bool verified;
        public string bio;
        public string birthday;
        public Cover[] cover;
        public Currency currency;
        public Device[] devices;
        public Education[] education;
        public string email;
        public NameInfo hometown;
        public string[] interested_in;
        public NameInfo location;
        public string political;
        public PaymentPricePoint[] payment_pricepoints;
        public NameInfo[] favorite_athletes;
        public NameInfo[] favorite_teams;
        public string picture;
        public string quotes;
        public string relationship_status;
        public string religion;
        public SecuritySettings security_settings;
        public NameInfo significant_other;
        public VideoUploadLimits video_upload_limits;
        public string website;
        public Work[] work;
    }

    public override socalAPIRequest.errorMessage parseError(string text){

        errorCode msg = JsonMapper.ToObject<errorCode>(text);

        if (null != msg)
        {
            if (0 != msg.code)
            {
                socalAPIRequest.errorMessage error = new socalAPIRequest.errorMessage();
                error.Message = String.Copy(msg.message);
                error.Code = msg.code;
                return error;
            }
        }
        return null;
    }

    public override bool isAuthFail(int error){
        switch (error){
        case 21319:
            return true;
        default:
            return false;
        }
    }

    public override bool postMessage(mesageScope scope, string message, int timeout, socalAPICallback callback){
        string url = "https://graph.facebook.com/me/feed";
        WWWForm form = new WWWForm();
        form.AddField("access_token",m_accesstoken);
        form.AddField("message",message);
        socalAPIRequest request = new socalAPIRequest(url, form, callback, timeout, this);
        socalAPIDispatcher.Instance().addCallback( request );

        return true;
    }



    public override bool postMessageWithPhoto( mesageScope scope, string message, string phtotFilePath, int timeout, socalAPICallback callback){
        // must create new Album
        string url = "https://graph.facebook.com/me/photos";
        WWWForm form = new WWWForm();
        form.AddField("access_token",m_accesstoken);
        form.AddField("message",message);

         byte[] fileData = loadFile(phtotFilePath);

        if ((fileData == null )||(fileData.Length ==0 ))
            return false;

        form.AddBinaryData("source", fileData );

        socalAPIRequest request = new socalAPIRequest(url, form, callback, timeout, this);
        socalAPIDispatcher.Instance().addCallback( request );

        return true;
    }

    public void getMe( int timeout, socalAPICallback callback){
        string url = "https://graph.facebook.com/me";
        url = String.Format("{0}?access_token={1}",url,m_accesstoken);
        socalAPIRequest request = new socalAPIRequest(url, null, callback, timeout, this);
        socalAPIDispatcher.Instance().addCallback( request );
    }
    protected override bool serialize(string data , bool store )    {
        bool resule = base.serialize(data , store);
        return resule;
    }

    string m_albumName;
    string m_albumId;
}
