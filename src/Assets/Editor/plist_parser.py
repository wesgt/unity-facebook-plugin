# -*- coding: utf-8 -*-
import xml.etree.ElementTree as ET
import ConfigParser


def set_facebook_to_plist(plist_path):
    # get fb_id fb_displayname
    config = ConfigParser.ConfigParser()
    config.read('Assets/Editor/fb_config.ini')
    fb_id = config.get('DEFAULT', 'FB_ID', 0)
    fb_displayname = config.get('DEFAULT', 'FB_DISPLAY_NAME', 0)
    print('fb_id : ' + fb_id)
    print('fb_displayname : ' + fb_displayname)

    info_plist_path = plist_path + '/Info.plist'
    plist_tree = ET.parse(info_plist_path)
    root = plist_tree.getroot()

    print(root.tag)
    print(root.attrib)

    dict_element = plist_tree.find('dict')

    print(dict_element.tag)
    print(dict_element.attrib)
    # for child in dict_element:
        # print(child.tag, child.attrib, child.text)

    # facebook_id
    fb_id_key = ET.SubElement(dict_element, 'key')
    fb_id_key.text = 'FacebookAppID'
    fb_id_string = ET.SubElement(dict_element, 'string')
    fb_id_string.text = fb_id

    # facebook_displayname
    fb_displayname_key = ET.SubElement(dict_element, 'key')
    fb_displayname_key.text = 'FacebookDisplayName'
    fb_displayname_string = ET.SubElement(dict_element, 'string')
    fb_displayname_string.text = fb_displayname.decode('utf-8')

    # url_type
    url_type_key = ET.SubElement(dict_element, 'key')
    url_type_key.text = 'CFBundleURLTypes'
    url_type_array = ET.SubElement(dict_element, 'array')
    url_type_dict = ET.SubElement(url_type_array, 'dict')
    url_type_dict_key = ET.SubElement(url_type_dict, 'key')
    url_type_dict_key.text = 'CFBundleURLSchemes'
    url_type_dict_array = ET.SubElement(url_type_dict, 'array')
    url_type_dict_array_string = ET.SubElement(url_type_dict_array, 'string')
    url_type_dict_array_string.text = 'fb' + fb_id

    rewrite_plist(info_plist_path, plist_tree)
    return


def rewrite_plist(info_plist_path, plist_tree):
    # plist_tree.write('Info.plist', xml_declaration=True, encoding="UTF-8")
    with open(info_plist_path, 'w') as plist_file:
        plist_file.write(
            '<?xml version="1.0" encoding="UTF-8"?><!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">')

    with open(info_plist_path, 'ab') as plist_file:
        plist_tree.write(plist_file, encoding='utf-8')
