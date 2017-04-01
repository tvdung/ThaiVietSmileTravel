/*
 Copyright (c) 2007-2016, CKSource - Frederico Knabben. All rights reserved.
 For licensing, see LICENSE.html or http://cksource.com/ckfinder/license
 */

var config = {'role': '*',
    'resourceType' : '*',  // Tài nguyên (Images | Files | Flash)
    'folder' :'/',        // Folder ảnh hưởng
    'folderView': true,   // Quyền xem folder
    'folderCreate': true, // Quyền tạo folder
    'folderRename': true, // Quyền đổi tên folder
    'folderDelete': true, // Quyền delete folder
    'fileView': true,     // Quyền xem file
    'fileUpload': true,   // Quyền upload file
    'fileRename': true,   // Quyền đổi tên file
    'fileDelete': true    // Quyền xóa file
};

// Set your configuration options below.

// Examples:
// config.language = 'pl';
// config.skin = 'jquery-mobile';

CKFinder.define( config );
