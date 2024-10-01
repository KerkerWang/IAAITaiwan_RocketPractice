/*
 Copyright (c) 2007-2019, CKSource - Frederico Knabben. All rights reserved.
 For licensing, see LICENSE.html or https://ckeditor.com/sales/license/ckfinder
 */

var config = {};

// Set your configuration options below.

// Examples:
config.language = 'zh-tw';
// config.skin = 'jquery-mobile';
config.filebrowserBrowseUrl = '/CKFinderScripts/ckfinder.html';
config.filebrowserImageBrowseUrl = '/CKFinderScripts/ckfinder.html?type=Images';
config.filebrowserUploadUrl = '/CKFinderScripts/connector?command=QuickUpload&type=Files';
config.filebrowserImageUploadUrl = '/CKFinderScripts/connector?command=QuickUpload&type=Images';

CKFinder.define(config);