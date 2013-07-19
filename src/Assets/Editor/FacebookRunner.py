#!/usr/bin/python
# -*- coding: utf-8 -*-

import sys
import shutil
import plist_parser
import build_xcode_config

from mod_pbxproj import XcodeProject
projectPath = sys.argv[1]
projectPath = projectPath.replace('_;@#', ' ')

plist_parser.set_facebook_to_plist(projectPath)

if not build_xcode_config.is_append_mode(projectPath, 'FB_SETTING_STATE', 'is_fb_set'):
    project = XcodeProject.Load(
        projectPath + '/Unity-iPhone.xcodeproj/project.pbxproj')
    libraries_group = project.get_or_create_group('Libraries')
    project.add_file(projectPath + '/Libraries/FacebookUnityPlugin.h',
                     parent=libraries_group, tree='SDKROOT')
    project.add_file(projectPath + '/Libraries/FBAppCall.h',
                     parent=libraries_group, tree='SDKROOT')
    # project.add_folder(projectPath + '/Libraries', parent=libraries_group,
    # excludes=["^.m*$"],tree='SDKROOT')
    project.add_file(
        'System/Library/Frameworks/Accounts.framework', tree='SDKROOT')
    project.add_file(
        'System/Library/Frameworks/Security.framework', tree='SDKROOT')
    project.add_file('usr/lib/libsqlite3.0.dylib', tree='SDKROOT')
    project.add_file(
        'System/Library/Frameworks/AdSupport.framework', tree='SDKROOT')
    project.add_file(
        'System/Library/Frameworks/Social.framework', tree='SDKROOT')
    # project.add_file('/Users/usin/Documents/FacebookSDK/FacebookSDK.framework',
    # tree='SDKROOT')
    project.add_other_ldflags('-ObjC')
    project.saveFormat3_2()
    print('FacebookRunner end')
