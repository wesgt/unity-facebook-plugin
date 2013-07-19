/********************************************************************************
** Form generated from reading UI file 'widget.ui'
**
** Created by: Qt User Interface Compiler version 5.1.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_WIDGET_H
#define UI_WIDGET_H

#include <QtCore/QVariant>
#include <QtWebKitWidgets/QWebView>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QLineEdit>
#include <QtWidgets/QProgressBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QVBoxLayout>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_Widget
{
public:
    QPushButton *pushButton;
    QWidget *verticalLayoutWidget;
    QVBoxLayout *verticalLayout;
    QWebView *webView;
    QLineEdit *lineEdit;
    QProgressBar *progressBar;

    void setupUi(QWidget *Widget)
    {
        if (Widget->objectName().isEmpty())
            Widget->setObjectName(QStringLiteral("Widget"));
        Widget->resize(800, 600);
        QSizePolicy sizePolicy(QSizePolicy::Fixed, QSizePolicy::Fixed);
        sizePolicy.setHorizontalStretch(0);
        sizePolicy.setVerticalStretch(0);
        sizePolicy.setHeightForWidth(Widget->sizePolicy().hasHeightForWidth());
        Widget->setSizePolicy(sizePolicy);
        Widget->setMinimumSize(QSize(0, 0));
        pushButton = new QPushButton(Widget);
        pushButton->setObjectName(QStringLiteral("pushButton"));
        pushButton->setGeometry(QRect(530, 430, 75, 23));
        verticalLayoutWidget = new QWidget(Widget);
        verticalLayoutWidget->setObjectName(QStringLiteral("verticalLayoutWidget"));
        verticalLayoutWidget->setGeometry(QRect(10, 10, 781, 581));
        verticalLayout = new QVBoxLayout(verticalLayoutWidget);
        verticalLayout->setSpacing(6);
        verticalLayout->setContentsMargins(11, 11, 11, 11);
        verticalLayout->setObjectName(QStringLiteral("verticalLayout"));
        verticalLayout->setContentsMargins(0, 0, 0, 0);
        webView = new QWebView(verticalLayoutWidget);
        webView->setObjectName(QStringLiteral("webView"));
        webView->setEnabled(true);
        webView->setUrl(QUrl(QStringLiteral("about:blank")));

        verticalLayout->addWidget(webView);

        lineEdit = new QLineEdit(Widget);
        lineEdit->setObjectName(QStringLiteral("lineEdit"));
        lineEdit->setGeometry(QRect(20, 430, 511, 20));
        progressBar = new QProgressBar(Widget);
        progressBar->setObjectName(QStringLiteral("progressBar"));
        progressBar->setGeometry(QRect(10, 0, 780, 10));
        progressBar->setValue(0);
        lineEdit->raise();
        pushButton->raise();
        verticalLayoutWidget->raise();
        progressBar->raise();

        retranslateUi(Widget);

        QMetaObject::connectSlotsByName(Widget);
    } // setupUi

    void retranslateUi(QWidget *Widget)
    {
        Widget->setWindowTitle(QApplication::translate("Widget", "miniBrower", 0));
        pushButton->setText(QApplication::translate("Widget", "PushButton", 0));
    } // retranslateUi

};

namespace Ui {
    class Widget: public Ui_Widget {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_WIDGET_H
