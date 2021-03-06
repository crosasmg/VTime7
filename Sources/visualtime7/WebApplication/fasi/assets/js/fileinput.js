/*!
 * bootstrap-fileinput v4.4.5
 * http://plugins.krajee.com/file-input
 *
 * Author: Kartik Visweswaran
 * Copyright: 2014 - 2017, Kartik Visweswaran, Krajee.com
 *
 * Licensed under the BSD 3-Clause
 * https://github.com/kartik-v/bootstrap-fileinput/blob/master/LICENSE.md
 */
!function (e) {
    "use strict";
    "function" == typeof define && define.amd ? define(["jquery"], e) : "object" == typeof module && module.exports ? module.exports = e(require("jquery")) : e(window.jQuery)
}(function (e) {
    "use strict";
    e.fn.fileinputLocales = {},
        e.fn.fileinputThemes = {},
        String.prototype.setTokens = function (e) {
            var t, i, a = this.toString();
            for (t in e)
                e.hasOwnProperty(t) && (i = new RegExp("{" + t + "}", "g"),
                    a = a.replace(i, e[t]));
            return a
        }
        ;
    var t, i;
    t = {
        FRAMES: ".kv-preview-thumb",
        SORT_CSS: "file-sortable",
        OBJECT_PARAMS: '<param name="controller" value="true" />\n<param name="allowFullScreen" value="true" />\n<param name="allowScriptAccess" value="always" />\n<param name="autoPlay" value="false" />\n<param name="autoStart" value="false" />\n<param name="quality" value="high" />\n',
        DEFAULT_PREVIEW: '<div class="file-preview-other">\n<span class="{previewFileIconClass}">{previewFileIcon}</span>\n</div>',
        MODAL_ID: "kvFileinputModal",
        MODAL_EVENTS: ["show", "shown", "hide", "hidden", "loaded"],
        objUrl: window.URL || window.webkitURL,
        compare: function (e, t, i) {
            return void 0 !== e && (i ? e === t : e.match(t))
        },
        isIE: function (e) {
            if ("Microsoft Internet Explorer" !== navigator.appName)
                return !1;
            if (10 === e)
                return new RegExp("msie\\s" + e, "i").test(navigator.userAgent);
            var t, i = document.createElement("div");
            return i.innerHTML = "<!--[if IE " + e + "]> <i></i> <![endif]-->",
                t = i.getElementsByTagName("i").length,
                document.body.appendChild(i),
                i.parentNode.removeChild(i),
                t
        },
        initModal: function (t) {
            var i = e("body");
            i.length && t.appendTo(i)
        },
        isEmpty: function (t, i) {
            return void 0 === t || null === t || 0 === t.length || i && "" === e.trim(t)
        },
        isArray: function (e) {
            return Array.isArray(e) || "[object Array]" === Object.prototype.toString.call(e)
        },
        ifSet: function (e, t, i) {
            return i = i || "",
                t && "object" == typeof t && e in t ? t[e] : i
        },
        cleanArray: function (e) {
            return e instanceof Array || (e = []),
                e.filter(function (e) {
                    return void 0 !== e && null !== e
                })
        },
        spliceArray: function (e, t) {
            var i, a = 0, n = [];
            if (!(e instanceof Array))
                return [];
            for (i = 0; i < e.length; i++)
                i !== t && (n[a] = e[i],
                    a++);
            return n
        },
        getNum: function (e, t) {
            return t = t || 0,
                "number" == typeof e ? e : ("string" == typeof e && (e = parseFloat(e)),
                    isNaN(e) ? t : e)
        },
        hasFileAPISupport: function () {
            return !(!window.File || !window.FileReader)
        },
        hasDragDropSupport: function () {
            var e = document.createElement("div");
            return !t.isIE(9) && (void 0 !== e.draggable || void 0 !== e.ondragstart && void 0 !== e.ondrop)
        },
        hasFileUploadSupport: function () {
            return t.hasFileAPISupport() && window.FormData
        },
        hasBlobSupport: function () {
            try {
                return !!window.Blob && Boolean(new Blob)
            } catch (e) {
                return !1
            }
        },
        hasArrayBufferViewSupport: function () {
            try {
                return 100 === new Blob([new Uint8Array(100)]).size
            } catch (e) {
                return !1
            }
        },
        dataURI2Blob: function (e) {
            var i, a, n, r, o, l, s = window.BlobBuilder || window.WebKitBlobBuilder || window.MozBlobBuilder || window.MSBlobBuilder, d = t.hasBlobSupport(), c = (d || s) && window.atob && window.ArrayBuffer && window.Uint8Array;
            if (!c)
                return null;
            for (i = e.split(",")[0].indexOf("base64") >= 0 ? atob(e.split(",")[1]) : decodeURIComponent(e.split(",")[1]),
                a = new ArrayBuffer(i.length),
                n = new Uint8Array(a),
                r = 0; r < i.length; r += 1)
                n[r] = i.charCodeAt(r);
            return o = e.split(",")[0].split(":")[1].split(";")[0],
                d ? new Blob([t.hasArrayBufferViewSupport() ? n : a], {
                    type: o
                }) : (l = new s,
                    l.append(a),
                    l.getBlob(o))
        },
        arrayBuffer2String: function (e) {
            if (window.TextDecoder)
                return new TextDecoder("utf-8").decode(e);
            var t, i, a, n, r = Array.prototype.slice.apply(new Uint8Array(e)), o = "", l = 0;
            for (t = r.length; t > l;)
                switch (i = r[l++],
                i >> 4) {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        o += String.fromCharCode(i);
                        break;
                    case 12:
                    case 13:
                        a = r[l++],
                            o += String.fromCharCode((31 & i) << 6 | 63 & a);
                        break;
                    case 14:
                        a = r[l++],
                            n = r[l++],
                            o += String.fromCharCode((15 & i) << 12 | (63 & a) << 6 | (63 & n) << 0)
                }
            return o
        },
        isHtml: function (e) {
            var t = document.createElement("div");
            t.innerHTML = e;
            for (var i = t.childNodes, a = i.length; a--;)
                if (1 === i[a].nodeType)
                    return !0;
            return !1
        },
        isSvg: function (e) {
            return e.match(/^\s*<\?xml/i) && (e.match(/<!DOCTYPE svg/i) || e.match(/<svg/i))
        },
        getMimeType: function (e, t, i) {
            switch (e) {
                case "ffd8ffe0":
                case "ffd8ffe1":
                case "ffd8ffe2":
                    return "image/jpeg";
                case "89504E47":
                    return "image/png";
                case "47494638":
                    return "image/gif";
                case "49492a00":
                    return "image/tiff";
                case "52494646":
                    return "image/webp";
                case "66747970":
                    return "video/3gp";
                case "4f676753":
                    return "video/ogg";
                case "1a45dfa3":
                    return "video/mkv";
                case "000001ba":
                case "000001b3":
                    return "video/mpeg";
                case "3026b275":
                    return "video/wmv";
                case "25504446":
                    return "application/pdf";
                case "25215053":
                    return "application/ps";
                case "504b0304":
                case "504b0506":
                case "504b0508":
                    return "application/zip";
                case "377abcaf":
                    return "application/7z";
                case "75737461":
                    return "application/tar";
                case "7801730d":
                    return "application/dmg";
                default:
                    switch (e.substring(0, 6)) {
                        case "435753":
                            return "application/x-shockwave-flash";
                        case "494433":
                            return "audio/mp3";
                        case "425a68":
                            return "application/bzip";
                        default:
                            switch (e.substring(0, 4)) {
                                case "424d":
                                    return "image/bmp";
                                case "fffb":
                                    return "audio/mp3";
                                case "4d5a":
                                    return "application/exe";
                                case "1f9d":
                                case "1fa0":
                                    return "application/zip";
                                case "1f8b":
                                    return "application/gzip";
                                default:
                                    return t && !t.match(/[^\u0000-\u007f]/) ? "application/text-plain" : i
                            }
                    }
            }
        },
        addCss: function (e, t) {
            e.removeClass(t).addClass(t)
        },
        getElement: function (i, a, n) {
            return t.isEmpty(i) || t.isEmpty(i[a]) ? n : e(i[a])
        },
        uniqId: function () {
            return Math.round((new Date).getTime()) + "_" + Math.round(100 * Math.random())
        },
        htmlEncode: function (e) {
            return e.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/"/g, "&quot;").replace(/'/g, "&apos;")
        },
        replaceTags: function (t, i) {
            var a = t;
            return i ? (e.each(i, function (e, t) {
                "function" == typeof t && (t = t()),
                    a = a.split(e).join(t)
            }),
                a) : a
        },
        cleanMemory: function (e) {
            var i = e.is("img") ? e.attr("src") : e.find("source").attr("src");
            t.objUrl.revokeObjectURL(i)
        },
        findFileName: function (e) {
            var t = e.lastIndexOf("/");
            return -1 === t && (t = e.lastIndexOf("\\")),
                e.split(e.substring(t, t + 1)).pop()
        },
        checkFullScreen: function () {
            return document.fullscreenElement || document.mozFullScreenElement || document.webkitFullscreenElement || document.msFullscreenElement
        },
        toggleFullScreen: function (e) {
            var i = document
                , a = i.documentElement;
            a && e && !t.checkFullScreen() ? a.requestFullscreen ? a.requestFullscreen() : a.msRequestFullscreen ? a.msRequestFullscreen() : a.mozRequestFullScreen ? a.mozRequestFullScreen() : a.webkitRequestFullscreen && a.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT) : i.exitFullscreen ? i.exitFullscreen() : i.msExitFullscreen ? i.msExitFullscreen() : i.mozCancelFullScreen ? i.mozCancelFullScreen() : i.webkitExitFullscreen && i.webkitExitFullscreen()
        },
        moveArray: function (e, t, i) {
            if (i >= e.length)
                for (var a = i - e.length; a-- + 1;)
                    e.push(void 0);
            return e.splice(i, 0, e.splice(t, 1)[0]),
                e
        },
        cleanZoomCache: function (e) {
            var t = e.closest(".kv-zoom-cache-theme");
            t.length || (t = e.closest(".kv-zoom-cache")),
                t.remove()
        },
        setOrientation: function (e, t) {
            var i, a, n, r = new DataView(e), o = 0, l = 1;
            if (65496 !== r.getUint16(o) || e.length < 2)
                return void (t && t());
            for (o += 2,
                i = r.byteLength; i - 2 > o;)
                switch (a = r.getUint16(o),
                o += 2,
                a) {
                    case 65505:
                        n = r.getUint16(o),
                            i = n - o,
                            o += 2;
                        break;
                    case 274:
                        l = r.getUint16(o + 6, !1),
                            i = 0
                }
            t && t(l)
        },
        validateOrientation: function (e, i) {
            if (window.FileReader && window.DataView) {
                var a, n = new FileReader;
                n.onloadend = function () {
                    a = n.result,
                        t.setOrientation(a, i)
                }
                    ,
                    n.readAsArrayBuffer(e)
            }
        },
        adjustOrientedImage: function (e, t) {
            var i, a, n;
            if (e.hasClass("is-portrait-gt4")) {
                if (t)
                    return void e.css({
                        width: e.parent().height()
                    });
                e.css({
                    height: "auto",
                    width: e.height()
                }),
                    i = e.parent().offset().top,
                    a = e.offset().top,
                    n = i - a,
                    e.css("margin-top", n)
            }
        }
    },
        i = function (i, a) {
            var n = this;
            n.$element = e(i),
                n.$parent = n.$element.parent(),
                n._validate() && (n.isPreviewable = t.hasFileAPISupport(),
                    n.isIE9 = t.isIE(9),
                    n.isIE10 = t.isIE(10),
                    (n.isPreviewable || n.isIE9) && (n._init(a),
                        n._listen()),
                    n.$element.removeClass("file-loading"))
        }
        ,
        i.prototype = {
            constructor: i,
            _cleanup: function () {
                var e = this;
                e.reader = null,
                    e.formdata = {},
                    e.uploadCount = 0,
                    e.uploadStatus = {},
                    e.uploadLog = [],
                    e.uploadAsyncCount = 0,
                    e.loadedImages = [],
                    e.totalImagesCount = 0,
                    e.ajaxRequests = [],
                    e.clearStack(),
                    e.fileInputCleared = !1,
                    e.fileBatchCompleted = !0,
                    e.isPreviewable || (e.showPreview = !1),
                    e.isError = !1,
                    e.ajaxAborted = !1,
                    e.cancelling = !1
            },
            _init: function (i, a) {
                var n, r, o, l, s = this, d = s.$element;
                s.options = i,
                    e.each(i, function (e, i) {
                        switch (e) {
                            case "minFileCount":
                            case "maxFileCount":
                            case "minFileSize":
                            case "maxFileSize":
                            case "maxFilePreviewSize":
                            case "resizeImageQuality":
                            case "resizeIfSizeMoreThan":
                            case "progressUploadThreshold":
                            case "initialPreviewCount":
                            case "zoomModalHeight":
                            case "minImageHeight":
                            case "maxImageHeight":
                            case "minImageWidth":
                            case "maxImageWidth":
                                s[e] = t.getNum(i);
                                break;
                            default:
                                s[e] = i
                        }
                    }),
                    s.rtl && (l = s.previewZoomButtonIcons.prev,
                        s.previewZoomButtonIcons.prev = s.previewZoomButtonIcons.next,
                        s.previewZoomButtonIcons.next = l),
                    a || s._cleanup(),
                    s.$form = d.closest("form"),
                    s._initTemplateDefaults(),
                    s.uploadFileAttr = t.isEmpty(d.attr("name")) ? "file_data" : d.attr("name"),
                    o = s._getLayoutTemplate("progress"),
                    s.progressTemplate = o.replace("{class}", s.progressClass),
                    s.progressCompleteTemplate = o.replace("{class}", s.progressCompleteClass),
                    s.progressErrorTemplate = o.replace("{class}", s.progressErrorClass),
                    s.dropZoneEnabled = t.hasDragDropSupport() && s.dropZoneEnabled,
                    s.isDisabled = d.attr("disabled") || d.attr("readonly"),
                    s.isDisabled && d.attr("disabled", !0),
                    s.isAjaxUpload = t.hasFileUploadSupport() && !t.isEmpty(s.uploadUrl),
                    s.isClickable = s.browseOnZoneClick && s.showPreview && (s.isAjaxUpload && s.dropZoneEnabled || !t.isEmpty(s.defaultPreviewContent)),
                    s.slug = "function" == typeof i.slugCallback ? i.slugCallback : s._slugDefault,
                    s.mainTemplate = s.showCaption ? s._getLayoutTemplate("main1") : s._getLayoutTemplate("main2"),
                    s.captionTemplate = s._getLayoutTemplate("caption"),
                    s.previewGenericTemplate = s._getPreviewTemplate("generic"),
                    !s.imageCanvas && s.resizeImage && (s.maxImageWidth || s.maxImageHeight) && (s.imageCanvas = document.createElement("canvas"),
                        s.imageCanvasContext = s.imageCanvas.getContext("2d")),
                    t.isEmpty(d.attr("id")) && d.attr("id", t.uniqId()),
                    s.namespace = ".fileinput_" + d.attr("id").replace(/-/g, "_"),
                    void 0 === s.$container ? s.$container = s._createContainer() : s._refreshContainer(),
                    r = s.$container,
                    s.$dropZone = r.find(".file-drop-zone"),
                    s.$progress = r.find(".kv-upload-progress"),
                    s.$btnUpload = r.find(".fileinput-upload"),
                    s.$captionContainer = t.getElement(i, "elCaptionContainer", r.find(".file-caption")),
                    s.$caption = t.getElement(i, "elCaptionText", r.find(".file-caption-name")),
                    t.isEmpty(s.msgPlaceholder) || (n = d.attr("multiple") ? s.filePlural : s.fileSingle,
                        s.$caption.attr("placeholder", s.msgPlaceholder.replace("{files}", n))),
                    s.$captionIcon = s.$captionContainer.find(".file-caption-icon"),
                    s.mainClass.indexOf("input-group-lg") > -1 ? t.addCss(s.$captionIcon, "icon-lg") : s.$captionIcon.removeClass("icon-lg"),
                    s.$previewContainer = t.getElement(i, "elPreviewContainer", r.find(".file-preview")),
                    s.$preview = t.getElement(i, "elPreviewImage", r.find(".file-preview-thumbnails")),
                    s.$previewStatus = t.getElement(i, "elPreviewStatus", r.find(".file-preview-status")),
                    s.$errorContainer = t.getElement(i, "elErrorContainer", s.$previewContainer.find(".kv-fileinput-error")),
                    s._validateDisabled(),
                    t.isEmpty(s.msgErrorClass) || t.addCss(s.$errorContainer, s.msgErrorClass),
                    a || (s.$errorContainer.hide(),
                        s.previewInitId = "preview-" + t.uniqId(),
                        s._initPreviewCache(),
                        s._initPreview(!0),
                        s._initPreviewActions(),
                        s._setFileDropZoneTitle(),
                        s.$parent.hasClass("file-loading") && (s.$container.insertBefore(s.$parent),
                            s.$parent.remove())),
                    d.attr("disabled") && s.disable(),
                    s._initZoom(),
                    s.hideThumbnailContent && t.addCss(s.$preview, "hide-content")
            },
            _initTemplateDefaults: function () {
                var i, a, n, r, o, l, s, d, c, p, u, f, m, v, g, h, w, _, b, C, y, x, T, E, S, F, k, I, P, A, D, z, $, U, j, B, R, O = this;
                i = '{preview}\n<div class="kv-upload-progress kv-hidden"></div><div class="clearfix"></div>\n<div class="input-group {class}">\n  {caption}\n<div class="input-group-btn">\n      {remove}\n      {cancel}\n      {upload}\n      {browse}\n    </div>\n</div>',
                    a = '{preview}\n<div class="kv-upload-progress kv-hidden"></div>\n<div class="clearfix"></div>\n{remove}\n{cancel}\n{upload}\n{browse}\n',
                    n = '<div class="file-preview {class}">\n    {close}    <div class="{dropClass}">\n    <div class="file-preview-thumbnails">\n    </div>\n    <div class="clearfix"></div>    <div class="file-preview-status text-center text-success"></div>\n    <div class="kv-fileinput-error"></div>\n    </div>\n</div>',
                    o = '<button type="button" class="close fileinput-remove">&times;</button>\n',
                    r = '<i class="glyphicon glyphicon-file"></i>',
                    l = '<div class="file-caption form-control {class}" tabindex="500">\n  <span class="file-caption-icon"></span>\n  <input class="file-caption-name" onkeydown="return false;" onpaste="return false;">\n</div>',
                    s = '<button type="{type}" tabindex="500" title="{title}" class="{css}" {status}>{icon} {label}</button>',
                    d = '<a href="{href}" tabindex="500" title="{title}" class="{css}" {status}>{icon} {label}</a>',
                    c = '<div tabindex="500" class="{css}" {status}>{icon} {label}</div>',
                    p = '<div id="' + t.MODAL_ID + '" class="file-zoom-dialog modal fade" tabindex="-1" aria-labelledby="' + t.MODAL_ID + 'Label"></div>',
                    u = '<div class="modal-dialog modal-lg{rtl}" role="document">\n  <div class="modal-content">\n    <div class="modal-header">\n      <h5 class="modal-title">{heading}</h5>\n      <span class="kv-zoom-title"></span>\n      <div class="kv-zoom-actions">{toggleheader}{fullscreen}{borderless}{close}</div>\n    </div>\n    <div class="modal-body">\n      <div class="floating-buttons"></div>\n      <div class="kv-zoom-body file-zoom-content {zoomFrameClass}"></div>\n{prev} {next}\n    </div>\n  </div>\n</div>\n',
                    f = '<div class="progress">\n    <div class="{class}" role="progressbar" aria-valuenow="{percent}" aria-valuemin="0" aria-valuemax="100" style="width:{percent}%;">\n        {status}\n     </div>\n</div>',
                    m = " <samp>({sizeText})</samp>",
                    v = '<div class="file-thumbnail-footer">\n    <div class="file-footer-caption" title="{caption}">\n        <div class="file-caption-info">{caption}</div>\n        <div class="file-size-info">{size}</div>\n    </div>\n    {progress}\n{indicator}\n{actions}\n</div>',
                    g = '<div class="file-actions">\n    <div class="file-footer-buttons">\n        {download} {upload} {delete} {zoom} {other}    </div>\n</div>\n{drag}\n<div class="clearfix"></div>',
                    h = '<button type="button" class="kv-file-remove {removeClass}" title="{removeTitle}" {dataUrl}{dataKey}>{removeIcon}</button>\n',
                    w = '<button type="button" class="kv-file-upload {uploadClass}" title="{uploadTitle}">{uploadIcon}</button>',
                    _ = '<a href="{downloadUrl}" class="{downloadClass}" title="{downloadTitle}" download="{caption}">{downloadIcon}</a>',
                    b = '<button type="button" class="kv-file-zoom {zoomClass}" title="{zoomTitle}">{zoomIcon}</button>',
                    C = '<span class="file-drag-handle {dragClass}" title="{dragTitle}">{dragIcon}</span>',
                    y = '<div class="file-upload-indicator" title="{indicatorTitle}">{indicator}</div>',
                    x = '<div class="file-preview-frame {frameClass}" id="{previewId}" data-fileindex="{fileindex}" data-template="{template}"',
                    T = x + '><div class="kv-file-content">\n',
                    E = x + ' title="{caption}"><div class="kv-file-content">\n',
                    S = "</div>{footer}\n</div>\n",
                    F = "{content}\n",
                    k = '<div class="kv-preview-data file-preview-html" title="{caption}" {style}>{data}</div>\n',
                    I = '<img src="{data}" class="file-preview-image kv-preview-data" title="{caption}" alt="{caption}" {style}>\n',
                    P = '<textarea class="kv-preview-data file-preview-text" title="{caption}" readonly {style}>{data}</textarea>\n',
                    A = '<video class="kv-preview-data file-preview-video" controls {style}>\n<source src="{data}" type="{type}">\n' + t.DEFAULT_PREVIEW + "\n</video>\n",
                    D = '<audio class="kv-preview-data file-preview-audio" controls {style}>\n<source src="{data}" type="{type}">\n' + t.DEFAULT_PREVIEW + "\n</audio>\n",
                    z = '<embed class="kv-preview-data file-preview-flash" src="{data}" type="application/x-shockwave-flash" {style}>\n',
                    U = '<embed class="kv-preview-data file-preview-pdf" src="{data}" type="application/pdf" {style}>\n',
                    $ = '<object class="kv-preview-data file-preview-object file-object {typeCss}" data="{data}" type="{type}" {style}>\n<param name="movie" value="{caption}" />\n' + t.OBJECT_PARAMS + " " + t.DEFAULT_PREVIEW + "\n</object>\n",
                    j = '<div class="kv-preview-data file-preview-other-frame" {style}>\n' + t.DEFAULT_PREVIEW + "\n</div>\n",
                    B = '<div class="kv-zoom-cache" style="display:none">{zoomContent}</div>',
                    R = {
                        width: "100%",
                        height: "100%",
                        "min-height": "480px"
                    },
                    O.defaults = {
                        layoutTemplates: {
                            main1: i,
                            main2: a,
                            preview: n,
                            close: o,
                            fileIcon: r,
                            caption: l,
                            modalMain: p,
                            modal: u,
                            progress: f,
                            size: m,
                            footer: v,
                            indicator: y,
                            actions: g,
                            actionDelete: h,
                            actionUpload: w,
                            actionDownload: _,
                            actionZoom: b,
                            actionDrag: C,
                            btnDefault: s,
                            btnLink: d,
                            btnBrowse: c,
                            zoomCache: B
                        },
                        previewMarkupTags: {
                            tagBefore1: T,
                            tagBefore2: E,
                            tagAfter: S
                        },
                        previewContentTemplates: {
                            generic: F,
                            html: k,
                            image: I,
                            text: P,
                            video: A,
                            audio: D,
                            flash: z,
                            object: $,
                            pdf: U,
                            other: j
                        },
                        allowedPreviewTypes: ["image", "html", "text", "video", "audio", "flash", "pdf", "object"],
                        previewTemplates: {},
                        previewSettings: {
                            image: {
                                width: "auto",
                                height: "auto",
                                "max-width": "100%",
                                "max-height": "100%"
                            },
                            html: {
                                width: "213px",
                                height: "160px"
                            },
                            text: {
                                width: "213px",
                                height: "160px"
                            },
                            video: {
                                width: "213px",
                                height: "160px"
                            },
                            audio: {
                                width: "100%",
                                height: "30px"
                            },
                            flash: {
                                width: "213px",
                                height: "160px"
                            },
                            object: {
                                width: "213px",
                                height: "160px"
                            },
                            pdf: {
                                width: "213px",
                                height: "160px"
                            },
                            other: {
                                width: "213px",
                                height: "160px"
                            }
                        },
                        previewSettingsSmall: {
                            image: {
                                width: "auto",
                                height: "auto",
                                "max-width": "100%",
                                "max-height": "100%"
                            },
                            html: {
                                width: "100%",
                                height: "160px"
                            },
                            text: {
                                width: "100%",
                                height: "160px"
                            },
                            video: {
                                width: "100%",
                                height: "auto"
                            },
                            audio: {
                                width: "100%",
                                height: "30px"
                            },
                            flash: {
                                width: "100%",
                                height: "auto"
                            },
                            object: {
                                width: "100%",
                                height: "auto"
                            },
                            pdf: {
                                width: "100%",
                                height: "160px"
                            },
                            other: {
                                width: "100%",
                                height: "160px"
                            }
                        },
                        previewZoomSettings: {
                            image: {
                                width: "auto",
                                height: "auto",
                                "max-width": "100%",
                                "max-height": "100%"
                            },
                            html: R,
                            text: R,
                            video: {
                                width: "auto",
                                height: "100%",
                                "max-width": "100%"
                            },
                            audio: {
                                width: "100%",
                                height: "30px"
                            },
                            flash: {
                                width: "auto",
                                height: "480px"
                            },
                            object: {
                                width: "auto",
                                height: "100%",
                                "max-width": "100%",
                                "min-height": "480px"
                            },
                            pdf: R,
                            other: {
                                width: "auto",
                                height: "100%",
                                "min-height": "480px"
                            }
                        },
                        fileTypeSettings: {
                            image: function (e, i) {
                                return t.compare(e, "image.*") || t.compare(i, /\.(gif|png|jpe?g)$/i)
                            },
                            html: function (e, i) {
                                return t.compare(e, "text/html") || t.compare(i, /\.(htm|html)$/i)
                            },
                            text: function (e, i) {
                                return t.compare(e, "text.*") || t.compare(i, /\.(xml|javascript)$/i) || t.compare(i, /\.(txt|md|csv|nfo|ini|json|php|js|css)$/i)
                            },
                            video: function (e, i) {
                                return t.compare(e, "video.*") && (t.compare(e, /(ogg|mp4|mp?g|mov|webm|3gp)$/i) || t.compare(i, /\.(og?|mp4|webm|mp?g|mov|3gp)$/i))
                            },
                            audio: function (e, i) {
                                return t.compare(e, "audio.*") && (t.compare(i, /(ogg|mp3|mp?g|wav)$/i) || t.compare(i, /\.(og?|mp3|mp?g|wav)$/i))
                            },
                            flash: function (e, i) {
                                return t.compare(e, "application/x-shockwave-flash", !0) || t.compare(i, /\.(swf)$/i)
                            },
                            pdf: function (e, i) {
                                return t.compare(e, "application/pdf", !0) || t.compare(i, /\.(pdf)$/i)
                            },
                            object: function () {
                                return !0
                            },
                            other: function () {
                                return !0
                            }
                        },
                        fileActionSettings: {
                            showRemove: !0,
                            showUpload: !0,
                            showDownload: !0,
                            showZoom: !0,
                            showDrag: !0,
                            removeIcon: '<i class="glyphicon glyphicon-trash"></i>',
                            removeClass: "btn btn-kv btn-default btn-outline-secondary",
                            removeErrorClass: "btn btn-kv btn-danger",
                            removeTitle: "Remove file",
                            uploadIcon: '<i class="glyphicon glyphicon-upload"></i>',
                            uploadClass: "btn btn-kv btn-default btn-outline-secondary",
                            uploadTitle: "Upload file",
                            uploadRetryIcon: '<i class="glyphicon glyphicon-repeat"></i>',
                            uploadRetryTitle: "Retry upload",
                            downloadIcon: '<i class="glyphicon glyphicon-download"></i>',
                            downloadClass: "btn btn-kv btn-default btn-outline-secondary",
                            downloadTitle: "Download file",
                            zoomIcon: '<i class="glyphicon glyphicon-zoom-in"></i>',
                            zoomClass: "btn btn-kv btn-default btn-outline-secondary",
                            zoomTitle: "View Details",
                            dragIcon: '<i class="glyphicon glyphicon-move"></i>',
                            dragClass: "text-info",
                            dragTitle: "Move / Rearrange",
                            dragSettings: {},
                            indicatorNew: '<i class="glyphicon glyphicon-plus-sign text-warning"></i>',
                            indicatorSuccess: '<i class="glyphicon glyphicon-ok-sign text-success"></i>',
                            indicatorError: '<i class="glyphicon glyphicon-exclamation-sign text-danger"></i>',
                            indicatorLoading: '<i class="glyphicon glyphicon-hourglass text-muted"></i>',
                            indicatorNewTitle: "Not uploaded yet",
                            indicatorSuccessTitle: "Uploaded",
                            indicatorErrorTitle: "Upload Error",
                            indicatorLoadingTitle: "Uploading ..."
                        }
                    },
                    e.each(O.defaults, function (t, i) {
                        return "allowedPreviewTypes" === t ? void (void 0 === O.allowedPreviewTypes && (O.allowedPreviewTypes = i)) : void (O[t] = e.extend(!0, {}, i, O[t]))
                    }),
                    O._initPreviewTemplates()
            },
            _initPreviewTemplates: function () {
                var i, a = this, n = a.defaults, r = a.previewMarkupTags, o = r.tagAfter;
                e.each(n.previewContentTemplates, function (e, n) {
                    t.isEmpty(a.previewTemplates[e]) && (i = r.tagBefore2,
                        "generic" !== e && "image" !== e && "html" !== e && "text" !== e || (i = r.tagBefore1),
                        a.previewTemplates[e] = i + n + o)
                })
            },
            _initPreviewCache: function () {
                var i = this;
                i.previewCache = {
                    data: {},
                    init: function () {
                        var e = i.initialPreview;
                        e.length > 0 && !t.isArray(e) && (e = e.split(i.initialPreviewDelimiter)),
                            i.previewCache.data = {
                                content: e,
                                config: i.initialPreviewConfig,
                                tags: i.initialPreviewThumbTags
                            }
                    },
                    count: function () {
                        return i.previewCache.data && i.previewCache.data.content ? i.previewCache.data.content.length : 0
                    },
                    get: function (a, n) {
                        var r, o, l, s, d, c, p, u = "init_" + a, f = i.previewCache.data, m = f.config[a], v = f.content[a], g = i.previewInitId + "-" + u, h = t.ifSet("previewAsData", m, i.initialPreviewAsData), w = function (e, a, n, r, o, l, s, d, c) {
                            return d = " file-preview-initial " + t.SORT_CSS + (d ? " " + d : ""),
                                i._generatePreviewTemplate(e, a, n, r, o, !1, null, d, l, s, c)
                        };
                        return v ? (n = void 0 === n ? !0 : n,
                            l = t.ifSet("type", m, i.initialPreviewFileType || "generic"),
                            d = t.ifSet("filename", m, t.ifSet("caption", m)),
                            c = t.ifSet("filetype", m, l),
                            s = i.previewCache.footer(a, n, m && m.size || null),
                            p = t.ifSet("frameClass", m),
                            r = h ? w(l, v, d, c, g, s, u, p) : w("generic", v, d, c, g, s, u, p, l).setTokens({
                                content: f.content[a]
                            }),
                            f.tags.length && f.tags[a] && (r = t.replaceTags(r, f.tags[a])),
                            t.isEmpty(m) || t.isEmpty(m.frameAttr) || (o = e(document.createElement("div")).html(r),
                                o.find(".file-preview-initial").attr(m.frameAttr),
                                r = o.html(),
                                o.remove()),
                            r) : ""
                    },
                    add: function (e, a, n, r) {
                        var o, l = i.previewCache.data;
                        return t.isArray(e) || (e = e.split(i.initialPreviewDelimiter)),
                            r ? (o = l.content.push(e) - 1,
                                l.config[o] = a,
                                l.tags[o] = n) : (o = e.length - 1,
                                    l.content = e,
                                    l.config = a,
                                    l.tags = n),
                            i.previewCache.data = l,
                            o
                    },
                    set: function (e, a, n, r) {
                        var o, l, s = i.previewCache.data;
                        if (e && e.length && (t.isArray(e) || (e = e.split(i.initialPreviewDelimiter)),
                            l = e.filter(function (e) {
                                return null !== e
                            }),
                            l.length)) {
                            if (void 0 === s.content && (s.content = []),
                                void 0 === s.config && (s.config = []),
                                void 0 === s.tags && (s.tags = []),
                                r) {
                                for (o = 0; o < e.length; o++)
                                    e[o] && s.content.push(e[o]);
                                for (o = 0; o < a.length; o++)
                                    a[o] && s.config.push(a[o]);
                                for (o = 0; o < n.length; o++)
                                    n[o] && s.tags.push(n[o])
                            } else
                                s.content = e,
                                    s.config = a,
                                    s.tags = n;
                            i.previewCache.data = s
                        }
                    },
                    unset: function (e) {
                        var t = i.previewCache.count();
                        if (t) {
                            if (1 === t)
                                return i.previewCache.data.content = [],
                                    i.previewCache.data.config = [],
                                    i.previewCache.data.tags = [],
                                    i.initialPreview = [],
                                    i.initialPreviewConfig = [],
                                    void (i.initialPreviewThumbTags = []);
                            i.previewCache.data.content.splice(e, 1),
                                i.previewCache.data.config.splice(e, 1),
                                i.previewCache.data.tags.splice(e, 1)
                        }
                    },
                    out: function () {
                        var e, t, a = "", n = i.previewCache.count();
                        if (0 === n)
                            return {
                                content: "",
                                caption: ""
                            };
                        for (t = 0; n > t; t++)
                            a += i.previewCache.get(t);
                        return e = i._getMsgSelected(n),
                            {
                                content: a,
                                caption: e
                            }
                    },
                    footer: function (e, a, n) {
                        var r = i.previewCache.data || {};
                        if (t.isEmpty(r.content))
                            return "";
                        (t.isEmpty(r.config) || t.isEmpty(r.config[e])) && (r.config[e] = {}),
                            a = void 0 === a ? !0 : a;
                        var o, l = r.config[e], s = t.ifSet("caption", l), d = t.ifSet("width", l, "auto"), c = t.ifSet("url", l, !1), p = t.ifSet("key", l, null), u = i.fileActionSettings, f = i.initialPreviewShowDelete || !1, m = l.downloadUrl || i.initialPreviewDownloadUrl || "", v = l.filename || l.caption || "", g = !!m, h = t.ifSet("showDelete", l, t.ifSet("showDelete", u, f)), w = t.ifSet("showDownload", l, t.ifSet("showDownload", u, g)), _ = t.ifSet("showZoom", l, t.ifSet("showZoom", u, !0)), b = t.ifSet("showDrag", l, t.ifSet("showDrag", u, !0)), C = c === !1 && a;
                        return w = w && l.downloadUrl !== !1 && !!m,
                            o = i._renderFileActions(!1, w, h, _, b, C, c, p, !0, m, v),
                            i._getLayoutTemplate("footer").setTokens({
                                progress: i._renderThumbProgress(),
                                actions: o,
                                caption: s,
                                size: i._getSize(n),
                                width: d,
                                indicator: ""
                            })
                    }
                },
                    i.previewCache.init()
            },
            _handler: function (e, t, i) {
                var a = this
                    , n = a.namespace
                    , r = t.split(" ").join(n + " ") + n;
                e && e.length && e.off(r).on(r, i)
            },
            _log: function (e) {
                var t = this
                    , i = t.$element.attr("id");
                i && (e = '"' + i + '": ' + e),
                    "undefined" != typeof window.console.log ? window.console.log(e) : window.alert(e)
            },
            _validate: function () {
                var e = this
                    , t = "file" === e.$element.attr("type");
                return t || e._log('The input "type" must be set to "file" for initializing the "bootstrap-fileinput" plugin.'),
                    t
            },
            _errorsExist: function () {
                var t, i = this, a = i.$errorContainer.find("li");
                return a.length ? !0 : (t = e(document.createElement("div")).html(i.$errorContainer.html()),
                    t.find(".kv-error-close").remove(),
                    t.find("ul").remove(),
                    !!e.trim(t.text()).length)
            },
            _errorHandler: function (e, t) {
                var i = this
                    , a = e.target.error
                    , n = function (e) {
                        i._showError(e.replace("{name}", t))
                    };
                n(a.code === a.NOT_FOUND_ERR ? i.msgFileNotFound : a.code === a.SECURITY_ERR ? i.msgFileSecured : a.code === a.NOT_READABLE_ERR ? i.msgFileNotReadable : a.code === a.ABORT_ERR ? i.msgFilePreviewAborted : i.msgFilePreviewError)
            },
            _addError: function (e) {
                var t = this
                    , i = t.$errorContainer;
                e && i.length && (i.html(t.errorCloseButton + e),
                    t._handler(i.find(".kv-error-close"), "click", function () {
                        i.fadeOut("slow")
                    }))
            },
            _setValidationError: function (e) {
                var i = this;
                e = (e ? e + " " : "") + "has-error",
                    i.$container.removeClass(e).addClass("has-error"),
                    t.addCss(i.$captionContainer, "is-invalid")
            },
            _resetErrors: function (e) {
                var t = this
                    , i = t.$errorContainer;
                t.msgUploadEmpty = null;
                t.isError = !1,
                    t.$container.removeClass("has-error"),
                    t.$captionContainer.removeClass("is-invalid"),
                    i.html(""),
                    e ? i.fadeOut("slow") : i.hide()
            },
            _showFolderError: function (e) {
                var t, i = this, a = i.$errorContainer;
                e && (t = i.msgFoldersNotAllowed.replace("{n}", e),
                    i._addError(t),
                    i._setValidationError(),
                    a.fadeIn(800),
                    i._raise("filefoldererror", [e, t]))
            },
            _showUploadError: function (e, t, i) {
                var a = this
                    , n = a.$errorContainer
                    , r = i || "fileuploaderror"
                    , o = t && t.id ? '<li data-file-id="' + t.id + '">' + e + "</li>" : "<li>" + e + "</li>";
                return 0 === n.find("ul").length ? a._addError("<ul>" + o + "</ul>") : n.find("ul").append(o),
                    n.fadeIn(800),
                    a._raise(r, [t, e]),
                    a._setValidationError("file-input-new"),
                    !0
            },
            _showError: function (e, t, i) {
                var a = this
                    , n = a.$errorContainer
                    , r = i || "fileerror";
                return t = t || {},
                    t.reader = a.reader,
                    a._addError(e),
                    n.fadeIn(800),
                    a._raise(r, [t, e]),
                    a.isAjaxUpload || a._clearFileInput(),
                    a._setValidationError("file-input-new"),
                    a.$btnUpload.attr("disabled", !0),
                    !0
            },
            _noFilesError: function (e) {
                var t = this
                    , i = t.minFileCount > 1 ? t.filePlural : t.fileSingle
                    , a = t.msgFilesTooLess.replace("{n}", t.minFileCount).replace("{files}", i)
                    , n = t.$errorContainer;
                t._addError(a),
                    t.isError = !0,
                    t._updateFileDetails(0),
                    n.fadeIn(800),
                    t._raise("fileerror", [e, a]),
                    t._clearFileInput(),
                    t._setValidationError()
            },
            _parseError: function (t, i, a, n) {
                var r, o = this, l = e.trim(a + ""), s = void 0 !== i.responseJSON && void 0 !== i.responseJSON.error ? i.responseJSON.error : i.responseText;
                return o.cancelling && o.msgUploadAborted && (l = o.msgUploadAborted),
                    o.showAjaxErrorDetails && s && (s = e.trim(s.replace(/\n\s*\n/g, "\n")),
                        r = s.length ? "<pre>" + s + "</pre>" : "",
                        l += l ? r : s),
                    l || (l = o.msgAjaxError.replace("{operation}", t)),
                    o.cancelling = !1,
                    n ? "<b>" + n + ": </b>" + l : l
            },
            _parseFileType: function (e, i) {
                var a, n, r, o, l = this, s = l.allowedPreviewTypes || [];
                if ("application/text-plain" === e)
                    return "text";
                for (o = 0; o < s.length; o++)
                    if (r = s[o],
                        a = l.fileTypeSettings[r],
                        n = a(e, i) ? r : "",
                        !t.isEmpty(n))
                        return n;
                return "other"
            },
            _getPreviewIcon: function (t) {
                var i, a = this, n = null;
                return t && t.indexOf(".") > -1 && (i = t.split(".").pop(),
                    a.previewFileIconSettings && (n = a.previewFileIconSettings[i] || a.previewFileIconSettings[i.toLowerCase()] || null),
                    a.previewFileExtSettings && e.each(a.previewFileExtSettings, function (e, t) {
                        return a.previewFileIconSettings[e] && t(i) ? void (n = a.previewFileIconSettings[e]) : void 0
                    })),
                    n
            },
            _parseFilePreviewIcon: function (e, t) {
                var i = this
                    , a = i._getPreviewIcon(t) || i.previewFileIcon
                    , n = e;
                return n.indexOf("{previewFileIcon}") > -1 && (n = n.setTokens({
                    previewFileIconClass: i.previewFileIconClass,
                    previewFileIcon: a
                })),
                    n
            },
            _raise: function (t, i) {
                var a = this
                    , n = e.Event(t);
                if (void 0 !== i ? a.$element.trigger(n, i) : a.$element.trigger(n),
                    n.isDefaultPrevented() || n.result === !1)
                    return !1;
                switch (t) {
                    case "filebatchuploadcomplete":
                    case "filebatchuploadsuccess":
                    case "fileuploaded":
                    case "fileclear":
                    case "filecleared":
                    case "filereset":
                    case "fileerror":
                    case "filefoldererror":
                    case "fileuploaderror":
                    case "filebatchuploaderror":
                    case "filedeleteerror":
                    case "filecustomerror":
                    case "filesuccessremove":
                        break;
                    default:
                        a.ajaxAborted || (a.ajaxAborted = n.result)
                }
                return !0
            },
            _listenFullScreen: function (e) {
                var t, i, a = this, n = a.$modal;
                n && n.length && (t = n && n.find(".btn-fullscreen"),
                    i = n && n.find(".btn-borderless"),
                    t.length && i.length && (t.removeClass("active").attr("aria-pressed", "false"),
                        i.removeClass("active").attr("aria-pressed", "false"),
                        e ? t.addClass("active").attr("aria-pressed", "true") : i.addClass("active").attr("aria-pressed", "true"),
                        n.hasClass("file-zoom-fullscreen") ? a._maximizeZoomDialog() : e ? a._maximizeZoomDialog() : i.removeClass("active").attr("aria-pressed", "false")))
            },
            _listen: function () {
                var i, a = this, n = a.$element, r = a.$form, o = a.$container;
                a._handler(n, "change", e.proxy(a._change, a)),
                    a.showBrowse && a._handler(a.$btnFile, "click", e.proxy(a._browse, a)),
                    a._handler(o.find(".fileinput-remove:not([disabled])"), "click", e.proxy(a.clear, a)),
                    a._handler(o.find(".fileinput-cancel"), "click", e.proxy(a.cancel, a)),
                    a._initDragDrop(),
                    a._handler(r, "reset", e.proxy(a.clear, a)),
                    a.isAjaxUpload || a._handler(r, "submit", e.proxy(a._submitForm, a)),
                    a._handler(a.$container.find(".fileinput-upload"), "click", e.proxy(a._uploadClick, a)),
                    a._handler(e(window), "resize", function () {
                        a._listenFullScreen(screen.width === window.innerWidth && screen.height === window.innerHeight)
                    }),
                    i = "webkitfullscreenchange mozfullscreenchange fullscreenchange MSFullscreenChange",
                    a._handler(e(document), i, function () {
                        a._listenFullScreen(t.checkFullScreen())
                    }),
                    a._autoFitContent(),
                    a._initClickable()
            },
            _autoFitContent: function () {
                var t, i = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth, a = this, n = 400 > i ? a.previewSettingsSmall || a.defaults.previewSettingsSmall : a.previewSettings || a.defaults.previewSettings;
                e.each(n, function (e, i) {
                    t = ".file-preview-frame .file-preview-" + e,
                        a.$preview.find(t + ".kv-preview-data," + t + " .kv-preview-data").css(i)
                })
            },
            _initClickable: function () {
                var i, a = this;
                a.isClickable && (i = a.isAjaxUpload ? a.$dropZone : a.$preview.find(".file-default-preview"),
                    t.addCss(i, "clickable"),
                    i.attr("tabindex", -1),
                    a._handler(i, "click", function (t) {
                        var n = e(t.target);
                        n.parents(".file-preview-thumbnails").length && !n.parents(".file-default-preview").length || (a.$element.trigger("click"),
                            i.blur())
                    }))
            },
            _initDragDrop: function () {
                var t = this
                    , i = t.$dropZone;
                t.isAjaxUpload && t.dropZoneEnabled && t.showPreview && (t._handler(i, "dragenter dragover", e.proxy(t._zoneDragEnter, t)),
                    t._handler(i, "dragleave", e.proxy(t._zoneDragLeave, t)),
                    t._handler(i, "drop", e.proxy(t._zoneDrop, t)),
                    t._handler(e(document), "dragenter dragover drop", t._zoneDragDropInit))
            },
            _zoneDragDropInit: function (e) {
                e.stopPropagation(),
                    e.preventDefault()
            },
            _zoneDragEnter: function (i) {
                var a = this
                    , n = e.inArray("Files", i.originalEvent.dataTransfer.types) > -1;
                return a._zoneDragDropInit(i),
                    a.isDisabled || !n ? (i.originalEvent.dataTransfer.effectAllowed = "none",
                        void (i.originalEvent.dataTransfer.dropEffect = "none")) : void t.addCss(a.$dropZone, "file-highlighted")
            },
            _zoneDragLeave: function (e) {
                var t = this;
                t._zoneDragDropInit(e),
                    t.isDisabled || t.$dropZone.removeClass("file-highlighted")
            },
            _zoneDrop: function (e) {
                var i = this;
                e.preventDefault(),
                    i.isDisabled || t.isEmpty(e.originalEvent.dataTransfer.files) || (i._change(e, "dragdrop"),
                        i.$dropZone.removeClass("file-highlighted"))
            },
            _uploadClick: function (e) {
                var i, a = this, n = a.$container.find(".fileinput-upload"), r = !n.hasClass("disabled") && t.isEmpty(n.attr("disabled"));
                if (!e || !e.isDefaultPrevented()) {
                    if (!a.isAjaxUpload)
                        return void (r && "submit" !== n.attr("type") && (i = n.closest("form"),
                            i.length && i.trigger("submit"),
                            e.preventDefault()));
                    e.preventDefault(),
                        r && a.upload()
                }
            },
            _submitForm: function () {
                var e = this;
                return e._isFileSelectionValid() && !e._abort({})
            },
            _clearPreview: function () {
                var i = this
                    , a = i.$preview
                    , n = i.showUploadedThumbs ? i.getFrames(":not(.file-preview-success)") : i.getFrames();
                n.each(function () {
                    var i = e(this);
                    i.remove(),
                        t.cleanZoomCache(a.find("#zoom-" + i.attr("id")))
                }),
                    i.getFrames().length && i.showPreview || i._resetUpload(),
                    i._validateDefaultPreview()
            },
            _initSortable: function () {
                var i, a = this, n = a.$preview, r = "." + t.SORT_CSS;
                window.KvSortable && 0 !== n.find(r).length && (i = {
                    handle: ".drag-handle-init",
                    dataIdAttr: "data-preview-id",
                    scroll: !1,
                    draggable: r,
                    onSort: function (i) {
                        var n, r, o = i.oldIndex, l = i.newIndex;
                        a.initialPreview = t.moveArray(a.initialPreview, o, l),
                            a.initialPreviewConfig = t.moveArray(a.initialPreviewConfig, o, l),
                            a.previewCache.init();
                        for (var s = 0; s < a.initialPreviewConfig.length; s++)
                            null !== a.initialPreviewConfig[s] && (n = a.initialPreviewConfig[s].key,
                                r = e(".kv-file-remove[data-key='" + n + "']").closest(t.FRAMES),
                                r.attr("data-fileindex", "init_" + s).attr("data-fileindex", "init_" + s));
                        a._raise("filesorted", {
                            previewId: e(i.item).attr("id"),
                            oldIndex: o,
                            newIndex: l,
                            stack: a.initialPreviewConfig
                        })
                    }
                },
                    n.data("kvsortable") && n.kvsortable("destroy"),
                    e.extend(!0, i, a.fileActionSettings.dragSettings),
                    n.kvsortable(i))
            },
            _setPreviewContent: function (e) {
                var t = this;
                t.$preview.html(e),
                    t._autoFitContent()
            },
            _initPreview: function (e) {
                var i, a = this, n = a.initialCaption || "";
                return a.previewCache.count() ? (i = a.previewCache.out(),
                    n = e && a.initialCaption ? a.initialCaption : i.caption,
                    a._setPreviewContent(i.content),
                    a._setInitThumbAttr(),
                    a._setCaption(n),
                    a._initSortable(),
                    void (t.isEmpty(i.content) || a.$container.removeClass("file-input-new"))) : (a._clearPreview(),
                        void (e ? a._setCaption(n) : a._initCaption()))
            },
            _getZoomButton: function (e) {
                var t = this
                    , i = t.previewZoomButtonIcons[e]
                    , a = t.previewZoomButtonClasses[e]
                    , n = ' title="' + (t.previewZoomButtonTitles[e] || "") + '" '
                    , r = n + ("close" === e ? ' data-dismiss="modal" aria-hidden="true"' : "");
                return "fullscreen" !== e && "borderless" !== e && "toggleheader" !== e || (r += ' data-toggle="button" aria-pressed="false" autocomplete="off"'),
                    '<button type="button" class="' + a + " btn-" + e + '"' + r + ">" + i + "</button>"
            },
            _getModalContent: function () {
                var e = this;
                return e._getLayoutTemplate("modal").setTokens({
                    rtl: e.rtl ? " kv-rtl" : "",
                    zoomFrameClass: e.frameClass,
                    heading: e.msgZoomModalHeading,
                    prev: e._getZoomButton("prev"),
                    next: e._getZoomButton("next"),
                    toggleheader: e._getZoomButton("toggleheader"),
                    fullscreen: e._getZoomButton("fullscreen"),
                    borderless: e._getZoomButton("borderless"),
                    close: e._getZoomButton("close")
                })
            },
            _listenModalEvent: function (e) {
                var i = this
                    , a = i.$modal
                    , n = function (e) {
                        return {
                            sourceEvent: e,
                            previewId: a.data("previewId"),
                            modal: a
                        }
                    };
                a.on(e + ".bs.modal", function (r) {
                    var o = a.find(".btn-fullscreen")
                        , l = a.find(".btn-borderless");
                    i._raise("filezoom" + e, n(r)),
                        "shown" === e && (l.removeClass("active").attr("aria-pressed", "false"),
                            o.removeClass("active").attr("aria-pressed", "false"),
                            a.hasClass("file-zoom-fullscreen") && (i._maximizeZoomDialog(),
                                t.checkFullScreen() ? o.addClass("active").attr("aria-pressed", "true") : l.addClass("active").attr("aria-pressed", "true")))
                })
            },
            _initZoom: function () {
                var i, a = this, n = a._getLayoutTemplate("modalMain"), r = "#" + t.MODAL_ID;
                a.showPreview && (a.$modal = e(r),
                    a.$modal && a.$modal.length || (i = e(document.createElement("div")).html(n).insertAfter(a.$container),
                        a.$modal = e(r).insertBefore(i),
                        i.remove()),
                    t.initModal(a.$modal),
                    a.$modal.html(a._getModalContent()),
                    e.each(t.MODAL_EVENTS, function (e, t) {
                        a._listenModalEvent(t)
                    }))
            },
            _initZoomButtons: function () {
                var t, i, a = this, n = a.$modal.data("previewId") || "", r = a.getFrames().toArray(), o = r.length, l = a.$modal.find(".btn-prev"), s = a.$modal.find(".btn-next");
                return r.length < 2 ? (l.hide(),
                    void s.hide()) : (l.show(),
                        s.show(),
                        void (o && (t = e(r[0]),
                            i = e(r[o - 1]),
                            l.removeAttr("disabled"),
                            s.removeAttr("disabled"),
                            t.length && t.attr("id") === n && l.attr("disabled", !0),
                            i.length && i.attr("id") === n && s.attr("disabled", !0))))
            },
            _maximizeZoomDialog: function () {
                var t = this
                    , i = t.$modal
                    , a = i.find(".modal-header:visible")
                    , n = i.find(".modal-footer:visible")
                    , r = i.find(".modal-body")
                    , o = e(window).height()
                    , l = 0;
                i.addClass("file-zoom-fullscreen"),
                    a && a.length && (o -= a.outerHeight(!0)),
                    n && n.length && (o -= n.outerHeight(!0)),
                    r && r.length && (l = r.outerHeight(!0) - r.height(),
                        o -= l),
                    i.find(".kv-zoom-body").height(o)
            },
            _resizeZoomDialog: function (e) {
                var i = this
                    , a = i.$modal
                    , n = a.find(".btn-fullscreen")
                    , r = a.find(".btn-borderless");
                if (a.hasClass("file-zoom-fullscreen"))
                    t.toggleFullScreen(!1),
                        e ? n.hasClass("active") || (a.removeClass("file-zoom-fullscreen"),
                            i._resizeZoomDialog(!0),
                            r.hasClass("active") && r.removeClass("active").attr("aria-pressed", "false")) : n.hasClass("active") ? n.removeClass("active").attr("aria-pressed", "false") : (a.removeClass("file-zoom-fullscreen"),
                                i.$modal.find(".kv-zoom-body").css("height", i.zoomModalHeight));
                else {
                    if (!e)
                        return void i._maximizeZoomDialog();
                    t.toggleFullScreen(!0)
                }
                a.focus()
            },
            _setZoomContent: function (i, a) {
                var n, r, o, l, s, d, c, p, u, f, m = this, v = i.attr("id"), g = m.$modal, h = g.find(".btn-prev"), w = g.find(".btn-next"), _ = g.find(".btn-fullscreen"), b = g.find(".btn-borderless"), C = g.find(".btn-toggleheader"), y = m.$preview.find("#zoom-" + v);
                r = y.attr("data-template") || "generic",
                    n = y.find(".kv-file-content"),
                    o = n.length ? n.html() : "",
                    u = i.data("caption") || "",
                    f = i.data("size") || "",
                    l = u + " " + f,
                    g.find(".kv-zoom-title").attr("title", e("<div/>").html(l).text()).html(l),
                    s = g.find(".kv-zoom-body"),
                    g.removeClass("kv-single-content"),
                    a ? (p = s.addClass("file-thumb-loading").clone().insertAfter(s),
                        s.html(o).hide(),
                        p.fadeOut("fast", function () {
                            s.fadeIn("fast", function () {
                                s.removeClass("file-thumb-loading")
                            }),
                                p.remove()
                        })) : s.html(o),
                    c = m.previewZoomSettings[r],
                    c && (d = s.find(".kv-preview-data"),
                        t.addCss(d, "file-zoom-detail"),
                        e.each(c, function (e, t) {
                            d.css(e, t),
                                (d.attr("width") && "width" === e || d.attr("height") && "height" === e) && d.removeAttr(e)
                        })),
                    g.data("previewId", v);
                var x = s.find("img");
                x.length && t.adjustOrientedImage(x, !0),
                    m._handler(h, "click", function () {
                        m._zoomSlideShow("prev", v)
                    }),
                    m._handler(w, "click", function () {
                        m._zoomSlideShow("next", v)
                    }),
                    m._handler(_, "click", function () {
                        m._resizeZoomDialog(!0)
                    }),
                    m._handler(b, "click", function () {
                        m._resizeZoomDialog(!1)
                    }),
                    m._handler(C, "click", function () {
                        var e, t = g.find(".modal-header"), i = g.find(".modal-body .floating-buttons"), a = t.find(".kv-zoom-actions"), n = function (e) {
                            var i = m.$modal.find(".kv-zoom-body")
                                , a = m.zoomModalHeight;
                            g.hasClass("file-zoom-fullscreen") && (a = i.outerHeight(!0),
                                e || (a -= t.outerHeight(!0))),
                                i.css("height", e ? a + e : a)
                        };
                        t.is(":visible") ? (e = t.outerHeight(!0),
                            t.slideUp("slow", function () {
                                a.find(".btn").appendTo(i),
                                    n(e)
                            })) : (i.find(".btn").appendTo(a),
                                t.slideDown("slow", function () {
                                    n()
                                })),
                            g.focus()
                    }),
                    m._handler(g, "keydown", function (e) {
                        var t = e.which || e.keyCode;
                        37 !== t || h.attr("disabled") || m._zoomSlideShow("prev", v),
                            39 !== t || w.attr("disabled") || m._zoomSlideShow("next", v)
                    })
            },
            _zoomPreview: function (e) {
                var i, a = this, n = a.$modal;
                if (!e.length)
                    throw "Cannot zoom to detailed preview!";
                t.initModal(n),
                    n.html(a._getModalContent()),
                    i = e.closest(t.FRAMES),
                    a._setZoomContent(i),
                    n.modal("show"),
                    a._initZoomButtons()
            },
            _zoomSlideShow: function (t, i) {
                var a, n, r, o = this, l = o.$modal.find(".kv-zoom-actions .btn-" + t), s = o.getFrames().toArray(), d = s.length;
                if (!l.attr("disabled")) {
                    for (n = 0; d > n; n++)
                        if (e(s[n]).attr("id") === i) {
                            r = "prev" === t ? n - 1 : n + 1;
                            break
                        }
                    0 > r || r >= d || !s[r] || (a = e(s[r]),
                        a.length && o._setZoomContent(a, !0),
                        o._initZoomButtons(),
                        o._raise("filezoom" + t, {
                            previewId: i,
                            modal: o.$modal
                        }))
                }
            },
            _initZoomButton: function () {
                var t = this;
                t.$preview.find(".kv-file-zoom").each(function () {
                    var i = e(this);
                    t._handler(i, "click", function () {
                        t._zoomPreview(i)
                    })
                })
            },
            _clearObjects: function (t) {
                t.find("video audio").each(function () {
                    this.pause(),
                        e(this).remove()
                }),
                    t.find("img object div").each(function () {
                        e(this).remove()
                    })
            },
            _clearFileInput: function () {
                var i, a, n, r = this, o = r.$element;
                r.fileInputCleared = !0,
                    t.isEmpty(o.val()) || (r.isIE9 || r.isIE10 ? (i = o.closest("form"),
                        a = e(document.createElement("form")),
                        n = e(document.createElement("div")),
                        o.before(n),
                        i.length ? i.after(a) : n.after(a),
                        a.append(o).trigger("reset"),
                        n.before(o).remove(),
                        a.remove()) : o.val(""))
            },
            _resetUpload: function () {
                var e = this;
                e.uploadCache = {
                    content: [],
                    config: [],
                    tags: [],
                    append: !0
                },
                    e.uploadCount = 0,
                    e.uploadStatus = {},
                    e.uploadLog = [],
                    e.uploadAsyncCount = 0,
                    e.loadedImages = [],
                    e.totalImagesCount = 0,
                    e.$btnUpload.removeAttr("disabled"),
                    e._setProgress(0),
                    e.$progress.hide(),
                    e._resetErrors(!1),
                    e.ajaxAborted = !1,
                    e.ajaxRequests = [],
                    e._resetCanvas(),
                    e.cacheInitialPreview = {},
                    e.overwriteInitial && (e.initialPreview = [],
                        e.initialPreviewConfig = [],
                        e.initialPreviewThumbTags = [],
                        e.previewCache.data = {
                            content: [],
                            config: [],
                            tags: []
                        })
            },
            _resetCanvas: function () {
                var e = this;
                e.canvas && e.imageCanvasContext && e.imageCanvasContext.clearRect(0, 0, e.canvas.width, e.canvas.height)
            },
            _hasInitialPreview: function () {
                var e = this;
                return !e.overwriteInitial && e.previewCache.count()
            },
            _resetPreview: function () {
                var e, t, i = this;
                i.previewCache.count() ? (e = i.previewCache.out(),
                    i._setPreviewContent(e.content),
                    i._setInitThumbAttr(),
                    t = i.initialCaption ? i.initialCaption : e.caption,
                    i._setCaption(t)) : (i._clearPreview(),
                        i._initCaption()),
                    i.showPreview && (i._initZoom(),
                        i._initSortable())
            },
            _clearDefaultPreview: function () {
                var e = this;
                e.$preview.find(".file-default-preview").remove()
            },
            _validateDefaultPreview: function () {
                var e = this;
                e.showPreview && !t.isEmpty(e.defaultPreviewContent) && (e._setPreviewContent('<div class="file-default-preview">' + e.defaultPreviewContent + "</div>"),
                    e.$container.removeClass("file-input-new"),
                    e._initClickable())
            },
            _resetPreviewThumbs: function (e) {
                var t, i = this;
                return e ? (i._clearPreview(),
                    void i.clearStack()) : void (i._hasInitialPreview() ? (t = i.previewCache.out(),
                        i._setPreviewContent(t.content),
                        i._setInitThumbAttr(),
                        i._setCaption(t.caption),
                        i._initPreviewActions()) : i._clearPreview())
            },
            _getLayoutTemplate: function (e) {
                var i = this
                    , a = i.layoutTemplates[e];
                return t.isEmpty(i.customLayoutTags) ? a : t.replaceTags(a, i.customLayoutTags)
            },
            _getPreviewTemplate: function (e) {
                var i = this
                    , a = i.previewTemplates[e];
                return t.isEmpty(i.customPreviewTags) ? a : t.replaceTags(a, i.customPreviewTags)
            },
            _getOutData: function (e, t, i) {
                var a = this;
                return e = e || {},
                    t = t || {},
                    i = i || a.filestack.slice(0) || {},
                    {
                        form: a.formdata,
                        files: i,
                        filenames: a.filenames,
                        filescount: a.getFilesCount(),
                        extra: a._getExtraData(),
                        response: t,
                        reader: a.reader,
                        jqXHR: e
                    }
            },
            _getMsgSelected: function (e) {
                var t = this
                    , i = 1 === e ? t.fileSingle : t.filePlural;
                return e > 0 ? t.msgSelected.replace("{n}", e).replace("{files}", i) : t.msgNoFilesSelected
            },
            _getFrame: function (t) {
                var i = this
                    , a = e("#" + t);
                return a.length ? a : (i._log('Invalid thumb frame with id: "' + t + '".'),
                    null)
            },
            _getThumbs: function (e) {
                return e = e || "",
                    this.getFrames(":not(.file-preview-initial)" + e)
            },
            _getExtraData: function (e, t) {
                var i = this
                    , a = i.uploadExtraData;
                return "function" == typeof i.uploadExtraData && (a = i.uploadExtraData(e, t)),
                    a
            },
            _initXhr: function (e, t, i) {
                var a = this;
                return e.upload && e.upload.addEventListener("progress", function (e) {
                    var n = 0
                        , r = e.total
                        , o = e.loaded || e.position;
                    e.lengthComputable && (n = Math.floor(o / r * 100)),
                        t ? a._setAsyncUploadStatus(t, n, i) : a._setProgress(n)
                }, !1),
                    e
            },
            _mergeAjaxCallback: function (e, t, i) {
                var a, n = this, r = n.ajaxSettings, o = n.mergeAjaxCallbacks;
                "delete" === i && (r = n.ajaxDeleteSettings,
                    o = n.mergeAjaxDeleteCallbacks),
                    a = r[e],
                    o && "function" == typeof a ? "before" === o ? r[e] = function () {
                        a.apply(this, arguments),
                            t.apply(this, arguments)
                    }
                        : r[e] = function () {
                            t.apply(this, arguments),
                                a.apply(this, arguments)
                        }
                        : r[e] = t,
                    "delete" === i ? n.ajaxDeleteSettings = r : n.ajaxSettings = r
            },
            _ajaxSubmit: function (t, i, a, n, r, o) {
                var l, s = this;
                s._raise("filepreajax", [r, o]) && (s._uploadExtra(r, o),
                    s._mergeAjaxCallback("beforeSend", t),
                    s._mergeAjaxCallback("success", i),
                    s._mergeAjaxCallback("complete", a),
                    s._mergeAjaxCallback("error", n),
                    l = e.extend(!0, {}, {
                        xhr: function () {
                            var t = e.ajaxSettings.xhr();
                            return s._initXhr(t, r, s.getFileStack().length)
                        },
                        url: o && s.uploadUrlThumb ? s.uploadUrlThumb : s.uploadUrl,
                        type: "POST",
                        dataType: "json",
                        data: s.formdata,
                        cache: !1,
                        processData: !1,
                        contentType: !1
                    }, s.ajaxSettings),
                    s.ajaxRequests.push(e.ajax(l)))
            },
            _mergeArray: function (e, i) {
                var a = this
                    , n = t.cleanArray(a[e])
                    , r = t.cleanArray(i);
                a[e] = n.concat(r)
            },
            _initUploadSuccess: function (i, a, n) {
                var r, o, l, s, d, c, p, u, f, m = this;
                m.showPreview && "object" == typeof i && !e.isEmptyObject(i) && void 0 !== i.initialPreview && i.initialPreview.length > 0 && (m.hasInitData = !0,
                    c = i.initialPreview || [],
                    p = i.initialPreviewConfig || [],
                    u = i.initialPreviewThumbTags || [],
                    r = void 0 === i.append || i.append,
                    c.length > 0 && !t.isArray(c) && (c = c.split(m.initialPreviewDelimiter)),
                    m._mergeArray("initialPreview", c),
                    m._mergeArray("initialPreviewConfig", p),
                    m._mergeArray("initialPreviewThumbTags", u),
                    void 0 !== a ? n ? (f = a.attr("data-fileindex"),
                        m.uploadCache.content[f] = c[0],
                        m.uploadCache.config[f] = p[0] || [],
                        m.uploadCache.tags[f] = u[0] || [],
                        m.uploadCache.append = r) : (l = m.previewCache.add(c, p[0], u[0], r),
                            o = m.previewCache.get(l, !1),
                            s = e(document.createElement("div")).html(o).hide().insertAfter(a),
                            d = s.find(".kv-zoom-cache"),
                            d && d.length && d.insertAfter(a),
                            a.fadeOut("slow", function () {
                                var e = s.find(".file-preview-frame");
                                e && e.length && e.insertBefore(a).fadeIn("slow").css("display:inline-block"),
                                    m._initPreviewActions(),
                                    m._clearFileInput(),
                                    t.cleanZoomCache(m.$preview.find("#zoom-" + a.attr("id"))),
                                    a.remove(),
                                    s.remove(),
                                    m._initSortable()
                            })) : (m.previewCache.set(c, p, u, r),
                                m._initPreview(),
                                m._initPreviewActions()))
            },
            _initSuccessThumbs: function () {
                var i = this;
                i.showPreview && i._getThumbs(t.FRAMES + ".file-preview-success").each(function () {
                    var a = e(this)
                        , n = i.$preview
                        , r = a.find(".kv-file-remove");
                    r.removeAttr("disabled"),
                        i._handler(r, "click", function () {
                            var e = a.attr("id")
                                , r = i._raise("filesuccessremove", [e, a.attr("data-fileindex")]);
                            t.cleanMemory(a),
                                r !== !1 && a.fadeOut("slow", function () {
                                    t.cleanZoomCache(n.find("#zoom-" + e)),
                                        a.remove(),
                                        i.getFrames().length || i.reset()
                                })
                        })
                })
            },
            _checkAsyncComplete: function () {
                var t, i, a = this;
                for (i = 0; i < a.filestack.length; i++)
                    if (a.filestack[i] && (t = a.previewInitId + "-" + i,
                        -1 === e.inArray(t, a.uploadLog)))
                        return !1;
                return a.uploadAsyncCount === a.uploadLog.length
            },
            _uploadExtra: function (t, i) {
                var a = this
                    , n = a._getExtraData(t, i);
                0 !== n.length && e.each(n, function (e, t) {
                    a.formdata.append(e, t)
                })
            },
            _uploadSingle: function (i, a) {
                var n, r, o, l, s, d, c, p, u, f, m, v = this, g = v.getFileStack().length, h = new FormData, w = v.previewInitId + "-" + i, _ = v.filestack.length > 0 || !e.isEmptyObject(v.uploadExtraData), b = e("#" + w).find(".file-thumb-progress"), C = {
                    id: w,
                    index: i
                };
                v.formdata = h,
                    v.showPreview && (r = e("#" + w + ":not(.file-preview-initial)"),
                        l = r.find(".kv-file-upload"),
                        s = r.find(".kv-file-remove"),
                        b.show()),
                    0 === g || !_ || l && l.hasClass("disabled") || v._abort(C) || (m = function (e, t) {
                        d || v.updateStack(e, void 0),
                            v.uploadLog.push(t),
                            v._checkAsyncComplete() && (v.fileBatchCompleted = !0)
                    }
                        ,
                        o = function () {
                            var e, i, a, n = v.uploadCache, r = 0, o = v.cacheInitialPreview;
                            v.fileBatchCompleted && (o && o.content && (r = o.content.length),
                                setTimeout(function () {
                                    var l = 0 === v.getFileStack(!0).length;
                                    if (v.showPreview) {
                                        if (v.previewCache.set(n.content, n.config, n.tags, n.append),
                                            r) {
                                            for (i = 0; i < n.content.length; i++)
                                                a = i + r,
                                                    o.content[a] = n.content[i],
                                                    o.config.length && (o.config[a] = n.config[i]),
                                                    o.tags.length && (o.tags[a] = n.tags[i]);
                                            v.initialPreview = t.cleanArray(o.content),
                                                v.initialPreviewConfig = t.cleanArray(o.config),
                                                v.initialPreviewThumbTags = t.cleanArray(o.tags)
                                        } else
                                            v.initialPreview = n.content,
                                                v.initialPreviewConfig = n.config,
                                                v.initialPreviewThumbTags = n.tags;
                                        v.cacheInitialPreview = {},
                                            v.hasInitData && (v._initPreview(),
                                                v._initPreviewActions())
                                    }
                                    v.unlock(l),
                                        l && v._clearFileInput(),
                                        e = v.$preview.find(".file-preview-initial"),
                                        v.uploadAsync && e.length && (t.addCss(e, t.SORT_CSS),
                                            v._initSortable()),
                                        v._raise("filebatchuploadcomplete", [v.filestack, v._getExtraData()]),
                                        v.uploadCount = 0,
                                        v.uploadStatus = {},
                                        v.uploadLog = [],
                                        v._setProgress(101)
                                }, 100))
                        }
                        ,
                        c = function (o) {
                            n = v._getOutData(o),
                                v.fileBatchCompleted = !1,
                                v.showPreview && (r.hasClass("file-preview-success") || (v._setThumbStatus(r, "Loading"),
                                    t.addCss(r, "file-uploading")),
                                    l.attr("disabled", !0),
                                    s.attr("disabled", !0)),
                                a || v.lock(),
                                v._raise("filepreupload", [n, w, i]),
                                e.extend(!0, C, n),
                                v._abort(C) && (o.abort(),
                                    v._setProgressCancelled())
                        }
                        ,
                        p = function (o, s, c) {
                            var p = v.showPreview && r.attr("id") ? r.attr("id") : w;
                            n = v._getOutData(c, o),
                                e.extend(!0, C, n),
                                setTimeout(function () {
                                    t.isEmpty(o) || t.isEmpty(o.error) ? (v.showPreview && (v._setThumbStatus(r, "Success"),
                                        l.hide(),
                                        v._initUploadSuccess(o, r, a),
                                        v._setProgress(101, b)),
                                        v._raise("fileuploaded", [n, p, i]),
                                        a ? m(i, p) : v.updateStack(i, void 0)) : (d = !0,
                                            v._showUploadError(o.error, C),
                                            v._setPreviewError(r, i, v.filestack[i], v.retryErrorUploads),
                                            v.retryErrorUploads || l.hide(),
                                            a && m(i, p),
                                            v._setProgress(101, e("#" + p).find(".file-thumb-progress"), v.msgUploadError))
                                }, 100)
                        }
                        ,
                        u = function () {
                            setTimeout(function () {
                                v.showPreview && (l.removeAttr("disabled"),
                                    s.removeAttr("disabled"),
                                    r.removeClass("file-uploading")),
                                    a ? o() : (v.unlock(!1),
                                        v._clearFileInput()),
                                    v._initSuccessThumbs()
                            }, 100)
                        }
                        ,
                        f = function (t, n, o) {
                            var s = v.ajaxOperations.uploadThumb
                                , c = v._parseError(s, t, o, a && v.filestack[i].name ? v.filestack[i].name : null);
                            d = !0,
                                setTimeout(function () {
                                    a && m(i, w),
                                        v.uploadStatus[w] = 100,
                                        v._setPreviewError(r, i, v.filestack[i], v.retryErrorUploads),
                                        v.retryErrorUploads || l.hide(),
                                        e.extend(!0, C, v._getOutData(t)),
                                        v._setProgress(101, b, v.msgAjaxProgressError.replace("{operation}", s)),
                                        v._setProgress(101, e("#" + w).find(".file-thumb-progress"), v.msgUploadError),
                                        v._showUploadError(c, C)
                                }, 100)
                        }
                        ,
                        h.append(v.uploadFileAttr, v.filestack[i], v.filenames[i]),
                        h.append("file_id", i),
                        v._ajaxSubmit(c, p, u, f, w, i))
            },
            _uploadBatch: function () {
                var i, a, n, r, o, l = this, s = l.filestack, d = s.length, c = {}, p = l.filestack.length > 0 || !e.isEmptyObject(l.uploadExtraData);
                l.formdata = new FormData,
                    0 !== d && p && !l._abort(c) && (o = function () {
                        e.each(s, function (e) {
                            l.updateStack(e, void 0)
                        }),
                            l._clearFileInput()
                    }
                        ,
                        i = function (i) {
                            l.lock();
                            var a = l._getOutData(i);
                            l.showPreview && l._getThumbs().each(function () {
                                var i = e(this)
                                    , a = i.find(".kv-file-upload")
                                    , n = i.find(".kv-file-remove");
                                i.hasClass("file-preview-success") || (l._setThumbStatus(i, "Loading"),
                                    t.addCss(i, "file-uploading")),
                                    a.attr("disabled", !0),
                                    n.attr("disabled", !0)
                            }),
                                l._raise("filebatchpreupload", [a]),
                                l._abort(a) && (i.abort(),
                                    l._setProgressCancelled())
                        }
                        ,
                        a = function (i, a, n) {
                            var r = l._getOutData(n, i)
                                , s = 0
                                , d = l._getThumbs(":not(.file-preview-success)")
                                , c = t.isEmpty(i) || t.isEmpty(i.errorkeys) ? [] : i.errorkeys;
                            t.isEmpty(i) || t.isEmpty(i.error) ? (l._raise("filebatchuploadsuccess", [r]),
                                o(),
                                l.showPreview ? (d.each(function () {
                                    var t = e(this);
                                    l._setThumbStatus(t, "Success"),
                                        t.removeClass("file-uploading"),
                                        t.find(".kv-file-upload").hide().removeAttr("disabled")
                                }),
                                    l._initUploadSuccess(i)) : l.reset(),
                                l._setProgress(101)) : (l.showPreview && (d.each(function () {
                                    var t = e(this)
                                        , i = t.attr("data-fileindex");
                                    t.removeClass("file-uploading"),
                                        t.find(".kv-file-upload").removeAttr("disabled"),
                                        t.find(".kv-file-remove").removeAttr("disabled"),
                                        0 === c.length || -1 !== e.inArray(s, c) ? (l._setPreviewError(t, i, l.filestack[i], l.retryErrorUploads),
                                            l.retryErrorUploads || (t.find(".kv-file-upload").hide(),
                                                l.updateStack(i, void 0))) : (t.find(".kv-file-upload").hide(),
                                                    l._setThumbStatus(t, "Success"),
                                                    l.updateStack(i, void 0)),
                                        t.hasClass("file-preview-error") && !l.retryErrorUploads || s++
                                }),
                                    l._initUploadSuccess(i)),
                                    l._showUploadError(i.error, r, "filebatchuploaderror"),
                                    l._setProgress(101, l.$progress, l.msgUploadError))
                        }
                        ,
                        r = function () {
                            l.unlock(),
                                l._initSuccessThumbs(),
                                l._clearFileInput(),
                                l._raise("filebatchuploadcomplete", [l.filestack, l._getExtraData()])
                        }
                        ,
                        n = function (t, i, a) {
                            var n = l._getOutData(t)
                                , r = l.ajaxOperations.uploadBatch
                                , o = l._parseError(r, t, a);
                            l._showUploadError(o, n, "filebatchuploaderror"),
                                l.uploadFileCount = d - 1,
                                l.showPreview && (l._getThumbs().each(function () {
                                    var t = e(this)
                                        , i = t.attr("data-fileindex");
                                    t.removeClass("file-uploading"),
                                        void 0 !== l.filestack[i] && l._setPreviewError(t)
                                }),
                                    l._getThumbs().removeClass("file-uploading"),
                                    l._getThumbs(" .kv-file-upload").removeAttr("disabled"),
                                    l._getThumbs(" .kv-file-delete").removeAttr("disabled"),
                                    l._setProgress(101, l.$progress, l.msgAjaxProgressError.replace("{operation}", r)))
                        }
                        ,
                        e.each(s, function (e, i) {
                            t.isEmpty(s[e]) || l.formdata.append(l.uploadFileAttr, i, l.filenames[e])
                        }),
                        l._ajaxSubmit(i, a, r, n))
            },
            _uploadExtraOnly: function () {
                var e, i, a, n, r = this, o = {};
                r.formdata = new FormData,
                    r._abort(o) || (e = function (e) {
                        r.lock();
                        var t = r._getOutData(e);
                        r._raise("filebatchpreupload", [t]),
                            r._setProgress(50),
                            o.data = t,
                            o.xhr = e,
                            r._abort(o) && (e.abort(),
                                r._setProgressCancelled())
                    }
                        ,
                        i = function (e, i, a) {
                            var n = r._getOutData(a, e);
                            t.isEmpty(e) || t.isEmpty(e.error) ? (r._raise("filebatchuploadsuccess", [n]),
                                r._clearFileInput(),
                                r._initUploadSuccess(e),
                                r._setProgress(101)) : r._showUploadError(e.error, n, "filebatchuploaderror")
                        }
                        ,
                        a = function () {
                            r.unlock(),
                                r._clearFileInput(),
                                r._raise("filebatchuploadcomplete", [r.filestack, r._getExtraData()])
                        }
                        ,
                        n = function (e, t, i) {
                            var a = r._getOutData(e)
                                , n = r.ajaxOperations.uploadExtra
                                , l = r._parseError(n, e, i);
                            o.data = a,
                                r._showUploadError(l, a, "filebatchuploaderror"),
                                r._setProgress(101, r.$progress, r.msgAjaxProgressError.replace("{operation}", n))
                        }
                        ,
                        r._ajaxSubmit(e, i, a, n))
            },
            _deleteFileIndex: function (i) {
                var a = this
                    , n = i.attr("data-fileindex");
                "init_" === n.substring(0, 5) && (n = parseInt(n.replace("init_", "")),
                    a.initialPreview = t.spliceArray(a.initialPreview, n),
                    a.initialPreviewConfig = t.spliceArray(a.initialPreviewConfig, n),
                    a.initialPreviewThumbTags = t.spliceArray(a.initialPreviewThumbTags, n),
                    a.getFrames().each(function () {
                        var t = e(this)
                            , i = t.attr("data-fileindex");
                        "init_" === i.substring(0, 5) && (i = parseInt(i.replace("init_", "")),
                            i > n && (i-- ,
                                t.attr("data-fileindex", "init_" + i)))
                    }),
                    a.uploadAsync && (a.cacheInitialPreview = a.getPreview()))
            },
            _initFileActions: function () {
                var i = this
                    , a = i.$preview;
                i.showPreview && (i._initZoomButton(),
                    i.getFrames(" .kv-file-remove").each(function () {
                        var n, r, o, l, s = e(this), d = s.closest(t.FRAMES), c = d.attr("id"), p = d.attr("data-fileindex");
                        i._handler(s, "click", function () {
                            return l = i._raise("filepreremove", [c, p]),
                                l !== !1 && i._validateMinCount() ? (n = d.hasClass("file-preview-error"),
                                    t.cleanMemory(d),
                                    void d.fadeOut("slow", function () {
                                        t.cleanZoomCache(a.find("#zoom-" + c)),
                                            i.updateStack(p, void 0),
                                            i._clearObjects(d),
                                            d.remove(),
                                            c && n && i.$errorContainer.find('li[data-file-id="' + c + '"]').fadeOut("fast", function () {
                                                e(this).remove(),
                                                    i._errorsExist() || i._resetErrors()
                                            }),
                                            i._clearFileInput();
                                        var l = i.getFileStack(!0)
                                            , s = i.previewCache.count()
                                            , u = l.length
                                            , f = i.showPreview && i.getFrames().length;
                                        0 !== u || 0 !== s || f ? (r = s + u,
                                            o = r > 1 ? i._getMsgSelected(r) : l[0] ? i._getFileNames()[0] : "",
                                            i._setCaption(o)) : i.reset(),
                                            i._raise("fileremoved", [c, p])
                                    })) : !1
                        })
                    }),
                    i.getFrames(" .kv-file-upload").each(function () {
                        var a = e(this);
                        i._handler(a, "click", function () {
                            var e = a.closest(t.FRAMES)
                                , n = e.attr("data-fileindex");
                            i.$progress.hide(),
                                e.hasClass("file-preview-error") && !i.retryErrorUploads || i._uploadSingle(n, !1)
                        })
                    }))
            },
            _initPreviewActions: function () {
                var i = this
                    , a = i.$preview
                    , n = i.deleteExtraData || {}
                    , r = t.FRAMES + " .kv-file-remove"
                    , o = i.fileActionSettings
                    , l = o.removeClass
                    , s = o.removeErrorClass
                    , d = function () {
                        var e = i.isAjaxUpload ? i.previewCache.count() : i.$element.get(0).files.length;
                        a.find(t.FRAMES).length || e || (i._setCaption(""),
                            i.reset(),
                            i.initialCaption = "")
                    };
                i._initZoomButton(),
                    a.find(r).each(function () {
                        var r, o, c, p = e(this), u = p.data("url") || i.deleteUrl, f = p.data("key");
                        if (!t.isEmpty(u) && void 0 !== f) {
                            var m, v, g, h, w = p.closest(t.FRAMES), _ = i.previewCache.data, b = w.attr("data-fileindex");
                            b = parseInt(b.replace("init_", "")),
                                g = t.isEmpty(_.config) && t.isEmpty(_.config[b]) ? null : _.config[b],
                                h = t.isEmpty(g) || t.isEmpty(g.extra) ? n : g.extra,
                                "function" == typeof h && (h = h()),
                                v = {
                                    id: p.attr("id"),
                                    key: f,
                                    extra: h
                                },
                                r = function (e) {
                                    i.ajaxAborted = !1,
                                        i._raise("filepredelete", [f, e, h]),
                                        p.removeClass(s),
                                        i._abort() ? e.abort() : (t.addCss(w, "file-uploading"),
                                            t.addCss(p, "disabled " + l))
                                }
                                ,
                                o = function (e, n, r) {
                                    var o, c;
                                    return t.isEmpty(e) || t.isEmpty(e.error) ? (w.removeClass("file-uploading").addClass("file-deleted"),
                                        void w.fadeOut("slow", function () {
                                            b = parseInt(w.attr("data-fileindex").replace("init_", "")),
                                                i.previewCache.unset(b),
                                                o = i.previewCache.count(),
                                                c = o > 0 ? i._getMsgSelected(o) : "",
                                                i._deleteFileIndex(w),
                                                i._setCaption(c),
                                                i._raise("filedeleted", [f, r, h]),
                                                t.cleanZoomCache(a.find("#zoom-" + w.attr("id"))),
                                                i._clearObjects(w),
                                                w.remove(),
                                                d()
                                        })) : (v.jqXHR = r,
                                            v.response = e,
                                            i._showError(e.error, v, "filedeleteerror"),
                                            w.removeClass("file-uploading"),
                                            p.removeClass("disabled " + l).addClass(s),
                                            void d())
                                }
                                ,
                                c = function (e, t, a) {
                                    var n = i.ajaxOperations.deleteThumb
                                        , r = i._parseError(n, e, a);
                                    v.jqXHR = e,
                                        v.response = {},
                                        i._showError(r, v, "filedeleteerror"),
                                        w.removeClass("file-uploading"),
                                        p.removeClass("disabled " + l).addClass(s),
                                        d()
                                }
                                ,
                                i._mergeAjaxCallback("beforeSend", r, "delete"),
                                i._mergeAjaxCallback("success", o, "delete"),
                                i._mergeAjaxCallback("error", c, "delete"),
                                m = e.extend(!0, {}, {
                                    url: u,
                                    type: "POST",
                                    dataType: "json",
                                    data: e.extend(!0, {}, {
                                        key: f
                                    }, h)
                                }, i.ajaxDeleteSettings),
                                i._handler(p, "click", function () {
                                    return i._validateMinCount() ? (i.ajaxAborted = !1,
                                        i._raise("filebeforedelete", [f, h]),
                                        void (i.ajaxAborted instanceof Promise ? i.ajaxAborted.then(function (t) {
                                            t || e.ajax(m)
                                        }) : i.ajaxAborted || e.ajax(m))) : !1
                                })
                        }
                    })
            },
            _hideFileIcon: function () {
                var e = this;
                e.overwriteInitial && e.$captionContainer.removeClass("icon-visible")
            },
            _showFileIcon: function () {
                var e = this;
                t.addCss(e.$captionContainer, "icon-visible")
            },
            _getSize: function (t) {
                var i, a, n, r = this, o = parseFloat(t), l = r.fileSizeGetter;
                return e.isNumeric(t) && e.isNumeric(o) ? ("function" == typeof l ? n = l(o) : 0 === o ? n = "0.00 B" : (i = Math.floor(Math.log(o) / Math.log(1024)),
                    a = ["B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"],
                    n = 1 * (o / Math.pow(1024, i)).toFixed(2) + " " + a[i]),
                    r._getLayoutTemplate("size").replace("{sizeText}", n)) : ""
            },
            _generatePreviewTemplate: function (i, a, n, r, o, l, s, d, c, p, u) {
                var f, m, v = this, g = v.slug(n), h = "", w = "", _ = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth, b = 400 > _ ? v.previewSettingsSmall[i] || v.defaults.previewSettingsSmall[i] : v.previewSettings[i] || v.defaults.previewSettings[i], C = c || v._renderFileFooter(g, s, "auto", l), y = v._getPreviewIcon(n), x = "type-default", T = y && v.preferIconicPreview, E = y && v.preferIconicZoomPreview;
                return b && e.each(b, function (e, t) {
                    w += e + ":" + t + ";"
                }),
                    m = function (a, l, s, c) {
                        var f = s ? "zoom-" + o : o
                            , m = v._getPreviewTemplate(a)
                            , h = (d || "") + " " + c;
                        return v.frameClass && (h = v.frameClass + " " + h),
                            s && (h = h.replace(" " + t.SORT_CSS, "")),
                            m = v._parseFilePreviewIcon(m, n),
                            "text" === a && (l = t.htmlEncode(l)),
                            "object" !== i || r || e.each(v.defaults.fileTypeSettings, function (e, t) {
                                "object" !== e && "other" !== e && t(n, r) && (x = "type-" + e)
                            }),
                            m.setTokens({
                                previewId: f,
                                caption: g,
                                frameClass: h,
                                type: r,
                                fileindex: p,
                                typeCss: x,
                                footer: C,
                                data: l,
                                template: u || i,
                                style: w ? 'style="' + w + '"' : ""
                            })
                    }
                    ,
                    p = p || o.slice(o.lastIndexOf("-") + 1),
                    v.fileActionSettings.showZoom && (h = m(E ? "other" : i, a, !0, "kv-zoom-thumb")),
                    h = "\n" + v._getLayoutTemplate("zoomCache").replace("{zoomContent}", h),
                    f = m(T ? "other" : i, a, !1, "kv-preview-thumb"),
                    f + h
            },
            _previewDefault: function (i, a, n) {
                var r = this
                    , o = r.$preview;
                if (r.showPreview) {
                    var l, s = i ? i.name : "", d = i ? i.type : "", c = i.size || 0, p = r.slug(s), u = n === !0 && !r.isAjaxUpload, f = t.objUrl.createObjectURL(i);
                    r._clearDefaultPreview(),
                        l = r._generatePreviewTemplate("other", f, s, d, a, u, c),
                        o.append("\n" + l),
                        r._setThumbAttr(a, p, c),
                        n === !0 && r.isAjaxUpload && r._setThumbStatus(e("#" + a), "Error")
                }
            },
            _previewFile: function (e, i, a, n, r, o) {
                if (this.showPreview) {
                    var l, s = this, d = i ? i.name : "", c = o.type, p = o.name, u = s._parseFileType(c, d), f = s.allowedPreviewTypes, m = s.allowedPreviewMimeTypes, v = s.$preview, g = i.size || 0, h = f && f.indexOf(u) >= 0, w = m && -1 !== m.indexOf(c), _ = "text" === u || "html" === u || "image" === u ? a.target.result : r;
                    if ("html" === u && s.purifyHtml && window.DOMPurify && (_ = window.DOMPurify.sanitize(_)),
                        h || w) {
                        l = s._generatePreviewTemplate(u, _, d, c, n, !1, g),
                            s._clearDefaultPreview(),
                            v.append("\n" + l);
                        var b = v.find("#" + n + " img");
                        b.length && s.autoOrientImage ? t.validateOrientation(i, function (e) {
                            if (!e)
                                return void s._validateImage(n, p, c, g, _);
                            var a = v.find("#zoom-" + n + " img")
                                , r = "rotate-" + e;
                            e > 4 && (r += b.width() > b.height() ? " is-portrait-gt4" : " is-landscape-gt4"),
                                t.addCss(b, r),
                                t.addCss(a, r),
                                s._raise("fileimageoriented", {
                                    $img: b,
                                    file: i
                                }),
                                s._validateImage(n, p, c, g, _),
                                t.adjustOrientedImage(b)
                        }) : s._validateImage(n, p, c, g, _)
                    } else
                        s._previewDefault(i, n);
                    s._setThumbAttr(n, p, g),
                        s._initSortable()
                }
            },
            _setThumbAttr: function (t, i, a) {
                var n = this
                    , r = e("#" + t);
                r.length && (a = a && a > 0 ? n._getSize(a) : "",
                    r.data({
                        caption: i,
                        size: a
                    }))
            },
            _setInitThumbAttr: function () {
                var e, i, a, n, r = this, o = r.previewCache.data, l = r.previewCache.count();
                if (0 !== l)
                    for (var s = 0; l > s; s++)
                        e = o.config[s],
                            n = r.previewInitId + "-init_" + s,
                            i = t.ifSet("caption", e, t.ifSet("filename", e)),
                            a = t.ifSet("size", e),
                            r._setThumbAttr(n, i, a)
            },
            _slugDefault: function (e) {
                return t.isEmpty(e) ? "" : String(e).replace(/[\-\[\]\/\{}:;#%=\(\)\*\+\?\\\^\$\|<>&"']/g, "_")
            },
            _readFiles: function (i) {
                this.reader = new FileReader;
                var a, n = this, r = n.$element, o = n.$preview, l = n.reader, s = n.$previewContainer, d = n.$previewStatus, c = n.msgLoading, p = n.msgProgress, u = n.previewInitId, f = i.length, m = n.fileTypeSettings, v = n.filestack.length, g = n.allowedFileTypes, h = g ? g.length : 0, w = n.allowedFileExtensions, _ = t.isEmpty(w) ? "" : w.join(", "), b = n.maxFilePreviewSize && parseFloat(n.maxFilePreviewSize), C = o.length && (!b || isNaN(b)), y = function (t, r, o, l) {
                    var s, d = e.extend(!0, {}, n._getOutData({}, {}, i), {
                        id: o,
                        index: l
                    }), c = {
                        id: o,
                        index: l,
                        file: r,
                        files: i
                    };
                    n._previewDefault(r, o, !0),
                        n.isAjaxUpload ? (n.addToStack(void 0),
                            setTimeout(function () {
                                a(l + 1)
                            }, 100)) : f = 0,
                        n._initFileActions(),
                        s = e("#" + o),
                        s.find(".kv-file-upload").hide(),
                        n.removeFromPreviewOnError && s.remove(),
                        n.isError = n.isAjaxUpload ? n._showUploadError(t, d) : n._showError(t, c),
                        n._updateFileDetails(f)
                };
                n.loadedImages = [],
                    n.totalImagesCount = 0,
                    e.each(i, function (e, t) {
                        var i = n.fileTypeSettings.image;
                        i && i(t.type) && n.totalImagesCount++
                    }),
                    a = function (x) {
                        if (t.isEmpty(r.attr("multiple")) && (f = 1),
                            x >= f)
                            return n.isAjaxUpload && n.filestack.length > 0 ? n._raise("filebatchselected", [n.getFileStack()]) : n._raise("filebatchselected", [i]),
                                s.removeClass("file-thumb-loading"),
                                void d.html("");
                        var T, E, S, F, k, I, P, A, D, z, $, U, j = v + x, B = u + "-" + j, R = i[x], O = m.text, L = m.image, M = m.html, Z = R.name ? n.slug(R.name) : "", N = (R.size || 0) / 1e3, H = "", V = t.objUrl.createObjectURL(R), W = 0, q = "", K = 0, Y = function () {
                            var e = p.setTokens({
                                index: x + 1,
                                files: f,
                                percent: 50,
                                name: Z
                            });
                            setTimeout(function () {
                                d.html(e),
                                    n._updateFileDetails(f),
                                    a(x + 1)
                            }, 100),
                                n._raise("fileloaded", [R, B, x, l])
                        };
                        if (h > 0)
                            for (E = 0; h > E; E++)
                                I = g[E],
                                    P = n.msgFileTypes[I] || I,
                                    q += 0 === E ? P : ", " + P;
                        if (Z === !1)
                            return void a(x + 1);
                        if (0 === Z.length)
                            return S = n.msgInvalidFileName.replace("{name}", t.htmlEncode(R.name)),
                                void y(S, R, B, x);
                        if (t.isEmpty(w) || (H = new RegExp("\\.(" + w.join("|") + ")$", "i")),
                            T = N.toFixed(2),
                            n.maxFileSize > 0 && N > n.maxFileSize)
                            return S = n.msgSizeTooLarge.setTokens({
                                name: Z,
                                size: T,
                                maxSize: n.maxFileSize
                            }),
                                void y(S, R, B, x);
                        if (null !== n.minFileSize && N <= t.getNum(n.minFileSize))
                            return S = n.msgSizeTooSmall.setTokens({
                                name: Z,
                                size: T,
                                minSize: n.minFileSize
                            }),
                                void y(S, R, B, x);
                        if (!t.isEmpty(g) && t.isArray(g)) {
                            for (E = 0; E < g.length; E += 1)
                                F = g[E],
                                    A = m[F],
                                    W += A && "function" == typeof A && A(R.type, R.name) ? 1 : 0;
                            if (0 === W)
                                return S = n.msgInvalidFileType.setTokens({
                                    name: Z,
                                    types: q
                                }),
                                    void y(S, R, B, x)
                        }
                        return 0 !== W || t.isEmpty(w) || !t.isArray(w) || t.isEmpty(H) || (k = t.compare(Z, H),
                            W += t.isEmpty(k) ? 0 : k.length,
                            0 !== W) ? n.showPreview ? !C && N > b ? (n.addToStack(R),
                                s.addClass("file-thumb-loading"),
                                n._previewDefault(R, B),
                                n._initFileActions(),
                                n._updateFileDetails(f),
                                void a(x + 1)) : (o.length && void 0 !== FileReader ? (D = O(R.type, Z),
                                    z = M(R.type, Z),
                                    $ = L(R.type, Z),
                                    d.html(c.replace("{index}", x + 1).replace("{files}", f)),
                                    s.addClass("file-thumb-loading"),
                                    l.onerror = function (e) {
                                        n._errorHandler(e, Z)
                                    }
                                    ,
                                    l.onload = function (i) {
                                        var a, r, o, s, d, c = [], p = function (e) {
                                            var t = new FileReader;
                                            t.onerror = function (e) {
                                                n._errorHandler(e, Z)
                                            }
                                                ,
                                                t.onload = function (e) {
                                                    n._previewFile(x, R, e, B, V, o),
                                                        n._initFileActions(),
                                                        Y()
                                                }
                                                ,
                                                e ? t.readAsText(R, n.textEncoding) : t.readAsDataURL(R)
                                        };
                                        return o = {
                                            name: Z,
                                            type: R.type
                                        },
                                            e.each(m, function (e, t) {
                                                "object" !== e && "other" !== e && t(R.type, Z) && K++
                                            }),
                                            0 === K && (a = new Uint8Array(i.target.result),
                                                a.forEach(function (e) {
                                                    c.push(e.toString(16))
                                                }),
                                                r = c.join("").toLowerCase().substring(0, 8),
                                                d = t.getMimeType(r, "", ""),
                                                t.isEmpty(d) && (s = t.arrayBuffer2String(l.result),
                                                    d = t.isSvg(s) ? "image/svg+xml" : t.getMimeType(r, s, R.type)),
                                                o = {
                                                    name: Z,
                                                    type: d
                                                },
                                                D = O(d, ""),
                                                z = M(d, ""),
                                                $ = L(d, ""),
                                                U = D || z,
                                                U || $) ? void p(U) : (n._previewFile(x, R, i, B, V, o),
                                                    n._initFileActions(),
                                                    void Y())
                                    }
                                    ,
                                    l.onprogress = function (e) {
                                        if (e.lengthComputable) {
                                            var t = e.loaded / e.total * 100
                                                , i = Math.ceil(t);
                                            S = p.setTokens({
                                                index: x + 1,
                                                files: f,
                                                percent: i,
                                                name: Z
                                            }),
                                                setTimeout(function () {
                                                    d.html(S)
                                                }, 100)
                                        }
                                    }
                                    ,
                                    D || z ? l.readAsText(R, n.textEncoding) : $ ? l.readAsDataURL(R) : l.readAsArrayBuffer(R)) : (n._previewDefault(R, B),
                                        setTimeout(function () {
                                            a(x + 1),
                                                n._updateFileDetails(f)
                                        }, 100),
                                        n._raise("fileloaded", [R, B, x, l])),
                                    void n.addToStack(R)) : (n.isAjaxUpload && n.addToStack(R),
                                        setTimeout(function () {
                                            a(x + 1),
                                                n._updateFileDetails(f)
                                        }, 100),
                                        void n._raise("fileloaded", [R, B, x, l])) : (S = n.msgInvalidFileExtension.setTokens({
                                            name: Z,
                                            extensions: _
                                        }),
                                            void y(S, R, B, x))
                    }
                    ,
                    a(0),
                    n._updateFileDetails(f, !1)
            },
            _updateFileDetails: function (e) {
                var i = this
                    , a = i.$element
                    , n = i.getFileStack()
                    , r = t.isIE(9) && t.findFileName(a.val()) || a[0].files[0] && a[0].files[0].name || n.length && n[0].name || ""
                    , o = i.slug(r)
                    , l = i.isAjaxUpload ? n.length : e
                    , s = i.previewCache.count() + l
                    , d = 1 === l ? o : i._getMsgSelected(s);
                i.isError ? (i.$previewContainer.removeClass("file-thumb-loading"),
                    i.$previewStatus.html(""),
                    i.$captionContainer.removeClass("icon-visible")) : i._showFileIcon(),
                    i._setCaption(d, i.isError),
                    i.$container.removeClass("file-input-new file-input-ajax-new"),
                    1 === arguments.length && i._raise("fileselect", [e, o]),
                    i.previewCache.count() && i._initPreviewActions()
            },
            _setThumbStatus: function (e, t) {
                var i = this;
                if (i.showPreview) {
                    var a = "indicator" + t
                        , n = a + "Title"
                        , r = "file-preview-" + t.toLowerCase()
                        , o = e.find(".file-upload-indicator")
                        , l = i.fileActionSettings;
                    e.removeClass("file-preview-success file-preview-error file-preview-loading"),
                        "Success" === t && e.find(".file-drag-handle").remove(),
                        o.html(l[a]),
                        o.attr("title", l[n]),
                        e.addClass(r),
                        "Error" !== t || i.retryErrorUploads || e.find(".kv-file-upload").attr("disabled", !0)
                }
            },
            _setProgressCancelled: function () {
                var e = this;
                e._setProgress(101, e.$progress, e.msgCancelled)
            },
            _setProgress: function (e, i, a) {
                var n, r = this, o = Math.min(e, 100), l = r.progressUploadThreshold, s = 100 >= e ? r.progressTemplate : r.progressCompleteTemplate, d = 100 > o ? r.progressTemplate : a ? r.progressErrorTemplate : s;
                i = i || r.$progress,
                    t.isEmpty(d) || (n = l && o > l && 100 >= e ? d.setTokens({
                        percent: l,
                        status: r.msgUploadThreshold
                    }) : d.setTokens({
                        percent: o,
                        status: e > 100 ? r.msgUploadEnd : o + "%"
                    }),
                        i.html(n),
                        a && i.find('[role="progressbar"]').html(a))
            },
            _setFileDropZoneTitle: function () {
                var e, i = this, a = i.$container.find(".file-drop-zone"), n = i.dropZoneTitle;
                i.isClickable && (e = t.isEmpty(i.$element.attr("multiple")) ? i.fileSingle : i.filePlural,
                    n += i.dropZoneClickTitle.replace("{files}", e)),
                    a.find("." + i.dropZoneTitleClass).remove(),
                    i.isAjaxUpload && i.showPreview && 0 !== a.length && !(i.getFileStack().length > 0) && i.dropZoneEnabled && (0 === a.find(t.FRAMES).length && t.isEmpty(i.defaultPreviewContent) && a.prepend('<div class="' + i.dropZoneTitleClass + '">' + n + "</div>"),
                        i.$container.removeClass("file-input-new"),
                        t.addCss(i.$container, "file-input-ajax-new"))
            },
            _setAsyncUploadStatus: function (t, i, a) {
                var n = this
                    , r = 0;
                n._setProgress(i, e("#" + t).find(".file-thumb-progress")),
                    n.uploadStatus[t] = i,
                    e.each(n.uploadStatus, function (e, t) {
                        r += t
                    }),
                    n._setProgress(Math.floor(r / a))
            },
            _validateMinCount: function () {
                var e = this
                    , t = e.isAjaxUpload ? e.getFileStack().length : e.$element.get(0).files.length;
                return e.validateInitialCount && e.minFileCount > 0 && e._getFileCount(t - 1) < e.minFileCount ? (e._noFilesError({}),
                    !1) : !0
            },
            _getFileCount: function (e) {
                var t = this
                    , i = 0;
                return t.validateInitialCount && !t.overwriteInitial && (i = t.previewCache.count(),
                    e += i),
                    e
            },
            _getFileId: function (e) {
                var t, i = this, a = i.generateFileId;
                return "function" == typeof a ? a(e, event) : e ? (t = String(e.webkitRelativePath || e.fileName || e.name || null),
                    t ? e.size + "-" + t.replace(/[^0-9a-zA-Z_-]/gim, "") : null) : null
            },
            _getFileName: function (e) {
                return e && e.name ? this.slug(e.name) : void 0
            },
            _getFileIds: function (e) {
                var t = this;
                return t.fileids.filter(function (t) {
                    return e ? void 0 !== t : void 0 !== t && null !== t
                })
            },
            _getFileNames: function (e) {
                var t = this;
                return t.filenames.filter(function (t) {
                    return e ? void 0 !== t : void 0 !== t && null !== t
                })
            },
            _setPreviewError: function (e, t, i, a) {
                var n = this;
                if (void 0 !== t && n.updateStack(t, i),
                    n.showPreview) {
                    if (n.removeFromPreviewOnError && !a)
                        return void e.remove();
                    n._setThumbStatus(e, "Error"),
                        n._refreshUploadButton(e, a)
                }
            },
            _refreshUploadButton: function (e, t) {
                var i = this
                    , a = e.find(".kv-file-upload")
                    , n = i.fileActionSettings
                    , r = n.uploadIcon
                    , o = n.uploadTitle;
                a.length && (t && (r = n.uploadRetryIcon,
                    o = n.uploadRetryTitle),
                    a.attr("title", o).html(r))
            },
            _checkDimensions: function (e, i, a, n, r, o, l) {
                var s, d, c, p, u = this, f = "Small" === i ? "min" : "max", m = u[f + "Image" + o];
                !t.isEmpty(m) && a.length && (c = a[0],
                    d = "Width" === o ? c.naturalWidth || c.width : c.naturalHeight || c.height,
                    p = "Small" === i ? d >= m : m >= d,
                    p || (s = u["msgImage" + o + i].setTokens({
                        name: r,
                        size: m
                    }),
                        u._showUploadError(s, l),
                        u._setPreviewError(n, e, null)))
            },
            _validateImage: function (t, i, a, n, r) {
                var o, l, s, d, c = this, p = c.$preview, u = p.find("#" + t), f = u.attr("data-fileindex"), m = u.find("img");
                i = i || "Untitled",
                    m.one("load", function () {
                        l = u.width(),
                            s = p.width(),
                            l > s && m.css("width", "100%"),
                            o = {
                                ind: f,
                                id: t
                            },
                            c._checkDimensions(f, "Small", m, u, i, "Width", o),
                            c._checkDimensions(f, "Small", m, u, i, "Height", o),
                            c.resizeImage || (c._checkDimensions(f, "Large", m, u, i, "Width", o),
                                c._checkDimensions(f, "Large", m, u, i, "Height", o)),
                            c._raise("fileimageloaded", [t]),
                            d =   null,
                            c.loadedImages.push({
                                ind: f,
                                img: m,
                                thumb: u,
                                pid: t,
                                typ: a,
                                siz: n,
                                validated: !1,
                                imgData: r,
                                exifObj: d
                            }),
                            u.data("exif", d),
                            c._validateAllImages()
                    }).one("error", function () {
                        c._raise("fileimageloaderror", [t])
                    }).each(function () {
                        this.complete ? e(this).trigger("load") : this.error && e(this).trigger("error")
                    })
            },
            _validateAllImages: function () {
                var e, t, i, a = this, n = {
                    val: 0
                }, r = a.loadedImages.length, o = a.resizeIfSizeMoreThan;
                if (r === a.totalImagesCount && (a._raise("fileimagesloaded"),
                    a.resizeImage))
                    for (e = 0; e < a.loadedImages.length; e++)
                        t = a.loadedImages[e],
                            t.validated || (i = t.siz,
                                i && i > 1e3 * o && a._getResizedImage(t, n, r),
                                a.loadedImages[e].validated = !0)
            },
            _getResizedImage: function (i, a, n) {
                var r, o, l, s, d, c, p, u = this, f = e(i.img)[0], m = f.naturalWidth, v = f.naturalHeight, g = 1, h = u.maxImageWidth || m, w = u.maxImageHeight || v, _ = !(!m || !v), b = u.imageCanvas, C = u.imageCanvasContext, y = i.typ, x = i.pid, T = i.ind, E = i.thumb, S = i.exifObj;
                if (d = function (e, t, i) {
                    u.isAjaxUpload ? u._showUploadError(e, t, i) : u._showError(e, t, i),
                        u._setPreviewError(E, T)
                }
                    ,
                    (!u.filestack[T] || !_ || h >= m && w >= v) && (_ && u.filestack[T] && u._raise("fileimageresized", [x, T]),
                        a.val++ ,
                        a.val === n && u._raise("fileimagesresized"),
                        !_))
                    return void d(u.msgImageResizeError, {
                        id: x,
                        index: T
                    }, "fileimageresizeerror");
                y = y || u.resizeDefaultImageType,
                    o = m > h,
                    l = v > w,
                    g = "width" === u.resizePreference ? o ? h / m : l ? w / v : 1 : l ? w / v : o ? h / m : 1,
                    u._resetCanvas(),
                    m *= g,
                    v *= g,
                    b.width = m,
                    b.height = v;
                try {
                    C.drawImage(f, 0, 0, m, v),
                        s = b.toDataURL(y, u.resizeQuality),
                        S && (p = window.piexif.dump(S),
                            s = window.piexif.insert(p, s)),
                        r = t.dataURI2Blob(s),
                        u.filestack[T] = r,
                        u._raise("fileimageresized", [x, T]),
                        a.val++ ,
                        a.val === n && u._raise("fileimagesresized", [void 0, void 0]),
                        r instanceof Blob || d(u.msgImageResizeError, {
                            id: x,
                            index: T
                        }, "fileimageresizeerror")
                } catch (F) {
                    a.val++ ,
                        a.val === n && u._raise("fileimagesresized", [void 0, void 0]),
                        c = u.msgImageResizeException.replace("{errors}", F.message),
                        d(c, {
                            id: x,
                            index: T
                        }, "fileimageresizeexception")
                }
            },
            _initBrowse: function (e) {
                var t = this;
                t.showBrowse ? (t.$btnFile = e.find(".btn-file"),
                    t.$btnFile.append(t.$element)) : t.$element.hide()
            },
            _initCaption: function () {
                var e = this
                    , i = e.initialCaption || "";
                return e.overwriteInitial || t.isEmpty(i) ? (e.$caption.val(""),
                    !1) : (e._setCaption(i),
                        !0)
            },
            _setCaption: function (i, a) {
                var n, r, o, l, s, d = this, c = d.getFileStack();
                if (d.$caption.length) {
                    if (d.$captionContainer.removeClass("icon-visible"),
                        a)
                        n = e("<div>" + d.msgValidationError + "</div>").text(),
                            l = c.length,
                            s = l ? 1 === l && c[0] ? d._getFileNames()[0] : d._getMsgSelected(l) : d._getMsgSelected(d.msgNo),
                            r = t.isEmpty(i) ? s : i,
                            o = '<span class="' + d.msgValidationErrorClass + '">' + d.msgValidationErrorIcon + "</span>";
                    else {
                        if (t.isEmpty(i))
                            return;
                        n = e("<div>" + i + "</div>").text(),
                            r = n,
                            o = d._getLayoutTemplate("fileIcon")
                    }
                    d.$captionContainer.addClass("icon-visible"),
                        d.$caption.attr("title", n).val(r),
                        d.$captionIcon.html(o)
                }
            },
            _createContainer: function () {
                var t = this
                    , i = {
                        "class": "file-input file-input-new" + (t.rtl ? " kv-rtl" : "")
                    }
                    , a = e(document.createElement("div")).attr(i).html(t._renderMain());
                return t.$element.before(a),
                    t._initBrowse(a),
                    t.theme && a.addClass("theme-" + t.theme),
                    a
            },
            _refreshContainer: function () {
                var e = this
                    , t = e.$container;
                t.before(e.$element),
                    t.html(e._renderMain()),
                    e._initBrowse(t),
                    e._validateDisabled()
            },
            _validateDisabled: function () {
                var e = this;
                e.$caption.attr({
                    readonly: e.isDisabled
                })
            },
            _renderMain: function () {
                var e = this
                    , t = e.isAjaxUpload && e.dropZoneEnabled ? " file-drop-zone" : "file-drop-disabled"
                    , i = e.showClose ? e._getLayoutTemplate("close") : ""
                    , a = e.showPreview ? e._getLayoutTemplate("preview").setTokens({
                        "class": e.previewClass,
                        dropClass: t
                    }) : ""
                    , n = e.isDisabled ? e.captionClass + " file-caption-disabled" : e.captionClass
                    , r = e.captionTemplate.setTokens({
                        "class": n + " kv-fileinput-caption"
                    });
                return e.mainTemplate.setTokens({
                    "class": e.mainClass + (!e.showBrowse && e.showCaption ? " no-browse" : ""),
                    preview: a,
                    close: i,
                    caption: r,
                    upload: e._renderButton("upload"),
                    remove: e._renderButton("remove"),
                    cancel: e._renderButton("cancel"),
                    browse: e._renderButton("browse")
                })
            },
            _renderButton: function (e) {
                var i = this
                    , a = i._getLayoutTemplate("btnDefault")
                    , n = i[e + "Class"]
                    , r = i[e + "Title"]
                    , o = i[e + "Icon"]
                    , l = i[e + "Label"]
                    , s = i.isDisabled ? " disabled" : ""
                    , d = "button";
                switch (e) {
                    case "remove":
                        if (!i.showRemove)
                            return "";
                        break;
                    case "cancel":
                        if (!i.showCancel)
                            return "";
                        n += " kv-hidden";
                        break;
                    case "upload":
                        if (!i.showUpload)
                            return "";
                        i.isAjaxUpload && !i.isDisabled ? a = i._getLayoutTemplate("btnLink").replace("{href}", i.uploadUrl) : d = "submit";
                        break;
                    case "browse":
                        if (!i.showBrowse)
                            return "";
                        a = i._getLayoutTemplate("btnBrowse");
                        break;
                    default:
                        return ""
                }
                return n += "browse" === e ? " btn-file" : " fileinput-" + e + " fileinput-" + e + "-button",
                    t.isEmpty(l) || (l = ' <span class="' + i.buttonLabelClass + '">' + l + "</span>"),
                    a.setTokens({
                        type: d,
                        css: n,
                        title: r,
                        status: s,
                        icon: o,
                        label: l
                    })
            },
            _renderThumbProgress: function () {
                var e = this;
                return '<div class="file-thumb-progress kv-hidden">' + e.progressTemplate.setTokens({
                    percent: "0",
                    status: e.msgUploadBegin
                }) + "</div>"
            },
            _renderFileFooter: function (e, i, a, n) {
                var r, o = this, l = o.fileActionSettings, s = l.showRemove, d = l.showDrag, c = l.showUpload, p = l.showZoom, u = o._getLayoutTemplate("footer"), f = o._getLayoutTemplate("indicator"), m = n ? l.indicatorError : l.indicatorNew, v = n ? l.indicatorErrorTitle : l.indicatorNewTitle, g = f.setTokens({
                    indicator: m,
                    indicatorTitle: v
                });
                return i = o._getSize(i),
                    r = o.isAjaxUpload ? u.setTokens({
                        actions: o._renderFileActions(c, !1, s, p, d, !1, !1, !1),
                        caption: e,
                        size: i,
                        width: a,
                        progress: o._renderThumbProgress(),
                        indicator: g
                    }) : u.setTokens({
                        actions: o._renderFileActions(!1, !1, !1, p, d, !1, !1, !1),
                        caption: e,
                        size: i,
                        width: a,
                        progress: "",
                        indicator: g
                    }),
                    r = t.replaceTags(r, o.previewThumbTags)
            },
            _renderFileActions: function (e, t, i, a, n, r, o, l, s, d, c) {
                if (!(e || t || i || a || n))
                    return "";
                var p, u = this, f = o === !1 ? "" : ' data-url="' + o + '"', m = l === !1 ? "" : ' data-key="' + l + '"', v = "", g = "", h = "", w = "", _ = "", b = u._getLayoutTemplate("actions"), C = u.fileActionSettings, y = u.otherActionButtons.setTokens({
                    dataKey: m,
                    key: l
                }), x = r ? C.removeClass + " disabled" : C.removeClass;
                return i && (v = u._getLayoutTemplate("actionDelete").setTokens({
                    removeClass: x,
                    removeIcon: C.removeIcon,
                    removeTitle: C.removeTitle,
                    dataUrl: f,
                    dataKey: m,
                    key: l
                })),
                    e && (g = u._getLayoutTemplate("actionUpload").setTokens({
                        uploadClass: C.uploadClass,
                        uploadIcon: C.uploadIcon,
                        uploadTitle: C.uploadTitle
                    })),
                    t && (h = u._getLayoutTemplate("actionDownload").setTokens({
                        downloadClass: C.downloadClass,
                        downloadIcon: C.downloadIcon,
                        downloadTitle: C.downloadTitle,
                        downloadUrl: d || u.initialPreviewDownloadUrl
                    }),
                        h = h.setTokens({
                            filename: c,
                            key: l
                        })),
                    a && (w = u._getLayoutTemplate("actionZoom").setTokens({
                        zoomClass: C.zoomClass,
                        zoomIcon: C.zoomIcon,
                        zoomTitle: C.zoomTitle
                    })),
                    n && s && (p = "drag-handle-init " + C.dragClass,
                        _ = u._getLayoutTemplate("actionDrag").setTokens({
                            dragClass: p,
                            dragTitle: C.dragTitle,
                            dragIcon: C.dragIcon
                        })),
                    b.setTokens({
                        "delete": v,
                        upload: g,
                        download: h,
                        zoom: w,
                        drag: _,
                        other: y
                    })
            },
            _browse: function (e) {
                var t = this;
                t._raise("filebrowse"),
                    e && e.isDefaultPrevented() || (t.isError && !t.isAjaxUpload && t.clear(),
                        t.$captionContainer.focus())
            },
            _filterDuplicate: function (e, t, i) {
                var a = this
                    , n = a._getFileId(e);
                n && i && i.indexOf(n) > -1 || (i || (i = []),
                    t.push(e),
                    i.push(n))
            },
            _change: function (i) {
                var a = this
                    , n = a.$element;
                if (!a.isAjaxUpload && t.isEmpty(n.val()) && a.fileInputCleared)
                    return void (a.fileInputCleared = !1);
                a.fileInputCleared = !1;
                var r, o, l, s, d = [], c = arguments.length > 1, p = a.isAjaxUpload, u = c ? i.originalEvent.dataTransfer.files : n.get(0).files, f = a.filestack.length, m = t.isEmpty(n.attr("multiple")), v = m && f > 0, g = 0, h = a._getFileIds(), w = function (t, i, n, r) {
                    var o = e.extend(!0, {}, a._getOutData({}, {}, u), {
                        id: n,
                        index: r
                    })
                        , l = {
                            id: n,
                            index: r,
                            file: i,
                            files: u
                        };
                    return a.isAjaxUpload ? a._showUploadError(t, o) : a._showError(t, l)
                };
                if (a.reader = null,
                    a._resetUpload(),
                    a._hideFileIcon(),
                    a.isAjaxUpload && a.$container.find(".file-drop-zone ." + a.dropZoneTitleClass).remove(),
                    c ? e.each(u, function (e, t) {
                        t && !t.type && void 0 !== t.size && t.size % 4096 === 0 ? g++ : a._filterDuplicate(t, d, h)
                    }) : (u = i.target && void 0 === i.target.files ? i.target.value ? [{
                        name: i.target.value.replace(/^.+\\/, "")
                    }] : [] : i.target.files || {},
                        p ? e.each(u, function (e, t) {
                            a._filterDuplicate(t, d, h)
                        }) : d = u),
                    t.isEmpty(d) || 0 === d.length)
                    return p || a.clear(),
                        a._showFolderError(g),
                        void a._raise("fileselectnone");
                if (a._resetErrors(),
                    s = d.length,
                    o = a._getFileCount(a.isAjaxUpload ? a.getFileStack().length + s : s),
                    a.maxFileCount > 0 && o > a.maxFileCount) {
                    if (!a.autoReplace || s > a.maxFileCount)
                        return l = a.autoReplace && s > a.maxFileCount ? s : o,
                            r = a.msgFilesTooMany.replace("{m}", a.maxFileCount).replace("{n}", l),
                            a.isError = w(r, null, null, null),
                            a.$captionContainer.removeClass("icon-visible"),
                            a._setCaption("", !0),
                            void a.$container.removeClass("file-input-new file-input-ajax-new");
                    o > a.maxFileCount && a._resetPreviewThumbs(p)
                } else
                    !p || v ? (a._resetPreviewThumbs(!1),
                        v && a.clearStack()) : !p || 0 !== f || a.previewCache.count() && !a.overwriteInitial || a._resetPreviewThumbs(!0);
                a.isPreviewable ? a._readFiles(d) : a._updateFileDetails(1),
                    a._showFolderError(g)
            },
            _abort: function (t) {
                var i, a = this;
                return a.ajaxAborted && "object" == typeof a.ajaxAborted && void 0 !== a.ajaxAborted.message ? (i = e.extend(!0, {}, a._getOutData(), t),
                    i.abortData = a.ajaxAborted.data || {},
                    i.abortMessage = a.ajaxAborted.message,
                    a._setProgress(101, a.$progress, a.msgCancelled),
                    a._showUploadError(a.ajaxAborted.message, i, "filecustomerror"),
                    a.cancel(),
                    !0) : !1
            },
            _resetFileStack: function () {
                var i = this
                    , a = 0
                    , n = []
                    , r = []
                    , o = [];
                i._getThumbs().each(function () {
                    var l, s = e(this), d = s.attr("data-fileindex"), c = i.filestack[d], p = s.attr("id");
                    "-1" !== d && -1 !== d && (void 0 !== c ? (n[a] = c,
                        r[a] = i._getFileName(c),
                        o[a] = i._getFileId(c),
                        s.attr({
                            id: i.previewInitId + "-" + a,
                            "data-fileindex": a
                        }),
                        a++) : (l = "uploaded-" + t.uniqId(),
                            s.attr({
                                id: l,
                                "data-fileindex": "-1"
                            }),
                            i.$preview.find("#zoom-" + p).attr("id", "zoom-" + l)))
                }),
                    i.filestack = n,
                    i.filenames = r,
                    i.fileids = o
            },
            _isFileSelectionValid: function (e) {
                var t = this;
                return e = e || 0,
                    t.required && !t.getFilesCount() ? (t.$errorContainer.html(""),
                        t._showUploadError(t.msgFileRequired),
                        !1) : t.minFileCount > 0 && t._getFileCount(e) < t.minFileCount ? (t._noFilesError({}),
                            !1) : !0
            },
            clearStack: function () {
                var e = this;
                return e.filestack = [],
                    e.filenames = [],
                    e.fileids = [],
                    e.$element
            },
            updateStack: function (e, t) {
                var i = this;
                return i.filestack[e] = t,
                    i.filenames[e] = i._getFileName(t),
                    i.fileids[e] = t && i._getFileId(t) || null,
                    i.$element
            },
            addToStack: function (e) {
                var t = this;
                return t.filestack.push(e),
                    t.filenames.push(t._getFileName(e)),
                    t.fileids.push(t._getFileId(e)),
                    t.$element
            },
            getFileStack: function (e) {
                var t = this;
                return t.filestack.filter(function (t) {
                    return e ? void 0 !== t : void 0 !== t && null !== t
                })
            },
            getFilesCount: function () {
                var e = this
                    , t = e.isAjaxUpload ? e.getFileStack().length : e.$element.get(0).files.length;
                return e._getFileCount(t)
            },
            lock: function () {
                var e = this;
                return e._resetErrors(),
                    e.disable(),
                    e.showRemove && e.$container.find(".fileinput-remove").hide(),
                    e.showCancel && e.$container.find(".fileinput-cancel").show(),
                    e._raise("filelock", [e.filestack, e._getExtraData()]),
                    e.$element
            },
            unlock: function (e) {
                var t = this;
                return void 0 === e && (e = !0),
                    t.enable(),
                    t.showCancel && t.$container.find(".fileinput-cancel").hide(),
                    t.showRemove && t.$container.find(".fileinput-remove").show(),
                    e && t._resetFileStack(),
                    t._raise("fileunlock", [t.filestack, t._getExtraData()]),
                    t.$element
            },
            cancel: function () {
                var t, i = this, a = i.ajaxRequests, n = a.length;
                if (n > 0)
                    for (t = 0; n > t; t += 1)
                        i.cancelling = !0,
                            a[t].abort();
                return i._setProgressCancelled(),
                    i._getThumbs().each(function () {
                        var t = e(this)
                            , a = t.attr("data-fileindex");
                        t.removeClass("file-uploading"),
                            void 0 !== i.filestack[a] && (t.find(".kv-file-upload").removeClass("disabled").removeAttr("disabled"),
                                t.find(".kv-file-remove").removeClass("disabled").removeAttr("disabled")),
                            i.unlock()
                    }),
                    i.$element
            },
            clear: function () {
                var i, a = this;
                if (a._raise("fileclear"))
                    return a.$btnUpload.removeAttr("disabled"),
                        a._getThumbs().find("video,audio,img").each(function () {
                            t.cleanMemory(e(this))
                        }),
                        a._resetUpload(),
                        a.clearStack(),
                        a._clearFileInput(),
                        a._resetErrors(!0),
                        a._hasInitialPreview() ? (a._showFileIcon(),
                            a._resetPreview(),
                            a._initPreviewActions(),
                            a.$container.removeClass("file-input-new")) : (a._getThumbs().each(function () {
                                a._clearObjects(e(this))
                            }),
                                a.isAjaxUpload && (a.previewCache.data = {}),
                                a.$preview.html(""),
                                i = !a.overwriteInitial && a.initialCaption.length > 0 ? a.initialCaption : "",
                                a.$caption.attr("title", "").val(i),
                                t.addCss(a.$container, "file-input-new"),
                                a._validateDefaultPreview()),
                        0 === a.$container.find(t.FRAMES).length && (a._initCaption() || a.$captionContainer.removeClass("icon-visible")),
                        a._hideFileIcon(),
                        a._raise("filecleared"),
                        a.$captionContainer.focus(),
                        a._setFileDropZoneTitle(),
                        a.$element
            },
            reset: function () {
                var e = this;
                if (e._raise("filereset"))
                    return e._resetPreview(),
                        e.$container.find(".fileinput-filename").text(""),
                        t.addCss(e.$container, "file-input-new"),
                        (e.getFrames().length || e.isAjaxUpload && e.dropZoneEnabled) && e.$container.removeClass("file-input-new"),
                        e.clearStack(),
                        e.formdata = {},
                        e._setFileDropZoneTitle(),
                        e.$element
            },
            disable: function () {
                var e = this;
                return e.isDisabled = !0,
                    e._raise("filedisabled"),
                    e.$element.attr("disabled", "disabled"),
                    e.$container.find(".kv-fileinput-caption").addClass("file-caption-disabled"),
                    e.$container.find(".fileinput-remove, .fileinput-upload, .file-preview-frame button").attr("disabled", !0),
                    t.addCss(e.$container.find(".btn-file"), "disabled"),
                    e._initDragDrop(),
                    e.$element
            },
            enable: function (isDisabled) {
                var e = this;
                if (isDisabled === undefined) {
                    return e.isDisabled = !1,
                        e._raise("fileenabled"),
                        e.$element.removeAttr("disabled"),
                        e.$container.find(".kv-fileinput-caption").removeClass("file-caption-disabled"),
                        e.$container.find(".fileinput-remove, .fileinput-upload, .file-preview-frame button").removeAttr("disabled"),
                        e.$container.find(".btn-file").removeClass("disabled"),
                        e._initDragDrop(),
                        e.$element;
                } else {
                    return e._raise("fileenabled"),
                        e.$element.removeAttr("disabled"),
                        e.$container.find(".kv-fileinput-caption").removeClass("file-caption-disabled"),
                        e.$container.find(".fileinput-remove, .fileinput-upload, .file-preview-frame button").removeAttr("disabled"),
                        e.$container.find(".btn-file").removeClass("disabled"),
                        e._initDragDrop(),
                        e.$element;
                }
            },
            upload: function () {
                var i, a, n, r = this, o = r.getFileStack().length, l = !e.isEmptyObject(r._getExtraData());
                if (r.isAjaxUpload && !r.isDisabled && r._isFileSelectionValid(o)) {
                    if (r._resetUpload(),
                        0 === o && !l && r.msgUploadEmpty !== null)
                        return void r._showUploadError(r.msgUploadEmpty);
                    r.enable(1);
                    if (r.$progress.show(),
                        r.uploadCount = 0,
                        r.uploadStatus = {},
                        r.uploadLog = [],
                        r.lock(),
                        r._setProgress(2),
                        0 === o && l)
                        return void r._uploadExtraOnly();
                    if (n = r.filestack.length,
                        r.hasInitData = !1,
                        !r.uploadAsync)
                        return r._uploadBatch(),
                            r.$element;
                    for (a = r._getOutData(),
                        r._raise("filebatchpreupload", [a]),
                        r.fileBatchCompleted = !1,
                        r.uploadCache = {
                            content: [],
                            config: [],
                            tags: [],
                            append: !0
                        },
                        r.uploadAsyncCount = r.getFileStack().length,
                        i = 0; n > i; i++)
                        r.uploadCache.content[i] = null,
                            r.uploadCache.config[i] = null,
                            r.uploadCache.tags[i] = null;
                    for (r.$preview.find(".file-preview-initial").removeClass(t.SORT_CSS),
                        r._initSortable(),
                        r.cacheInitialPreview = r.getPreview(),
                        i = 0; n > i; i++)
                        r.filestack[i] && r._uploadSingle(i, !0)
                }
            },
            destroy: function () {
                var t = this
                    , i = t.$form
                    , a = t.$container
                    , n = t.$element
                    , r = t.namespace;
                return e(document).off(r),
                    e(window).off(r),
                    i && i.length && i.off(r),
                    t.isAjaxUpload && t._clearFileInput(),
                    t._cleanup(),
                    t._initPreviewCache(),
                    n.insertBefore(a).off(r).removeData(),
                    a.off().remove(),
                    n
            },
            refresh: function (i, a) {
                var n = this
                    , r = n.$element;
                return i = "object" != typeof i || t.isEmpty(i) ? n.options : e.extend(!0, {}, n.options, i),
                    n._init(i, !0),
                    n._listen(),
                    a && r.trigger("change" + n.namespace),
                    r
            },
            zoom: function (e) {
                var i = this
                    , a = i._getFrame(e)
                    , n = i.$modal;
                a && (t.initModal(n),
                    n.html(i._getModalContent()),
                    i._setZoomContent(a),
                    n.modal("show"),
                    i._initZoomButtons())
            },
            getExif: function (e) {
                var t = this
                    , i = t._getFrame(e);
                return i && i.data("exif") || null
            },
            getFrames: function (e) {
                var i = this;
                return e = e || "",
                    i.$preview.find(t.FRAMES + e)
            },
            getPreview: function () {
                var e = this;
                return {
                    content: e.initialPreview,
                    config: e.initialPreviewConfig,
                    tags: e.initialPreviewThumbTags
                }
            }
        },
        e.fn.fileinput = function (a) {
            if (t.hasFileAPISupport() || t.isIE(9)) {
                var n = Array.apply(null, arguments)
                    , r = [];
                switch (n.shift(),
                this.each(function () {
                    var o, l = e(this), s = l.data("fileinput"), d = "object" == typeof a && a, c = d.theme || l.data("theme"), p = {}, u = {}, f = d.language || l.data("language") || e.fn.fileinput.defaults.language || "en";
                    s || (c && (u = e.fn.fileinputThemes[c] || {}),
                        "en" === f || t.isEmpty(e.fn.fileinputLocales[f]) || (p = e.fn.fileinputLocales[f] || {}),
                        o = e.extend(!0, {}, e.fn.fileinput.defaults, u, e.fn.fileinputLocales.en, p, d, l.data()),
                        s = new i(this, o),
                        l.data("fileinput", s)),
                        "string" == typeof a && r.push(s[a].apply(s, n))
                }),
                r.length) {
                    case 0:
                        return this;
                    case 1:
                        return r[0];
                    default:
                        return r
                }
            }
        }
        ,
        e.fn.fileinput.defaults = {
            language: "en",
            showCaption: !0,
            showBrowse: !0,
            showPreview: !0,
            showRemove: !0,
            showUpload: !0,
            showCancel: !0,
            showClose: !0,
            showUploadedThumbs: !0,
            browseOnZoneClick: !1,
            autoReplace: !1,
            autoOrientImage: !0,
            required: !1,
            rtl: !1,
            hideThumbnailContent: !1,
            generateFileId: null,
            previewClass: "",
            captionClass: "",
            frameClass: "krajee-default",
            mainClass: "file-caption-main",
            mainTemplate: null,
            purifyHtml: !0,
            fileSizeGetter: null,
            initialCaption: "",
            initialPreview: [],
            initialPreviewDelimiter: "*$$*",
            initialPreviewAsData: !1,
            initialPreviewFileType: "image",
            initialPreviewConfig: [],
            initialPreviewThumbTags: [],
            previewThumbTags: {},
            initialPreviewShowDelete: !0,
            initialPreviewDownloadUrl: "",
            removeFromPreviewOnError: !1,
            deleteUrl: "",
            deleteExtraData: {},
            overwriteInitial: !0,
            previewZoomButtonIcons: {
                prev: '<i class="glyphicon glyphicon-triangle-left"></i>',
                next: '<i class="glyphicon glyphicon-triangle-right"></i>',
                toggleheader: '<i class="glyphicon glyphicon-resize-vertical"></i>',
                fullscreen: '<i class="glyphicon glyphicon-fullscreen"></i>',
                borderless: '<i class="glyphicon glyphicon-resize-full"></i>',
                close: '<i class="glyphicon glyphicon-remove"></i>'
            },
            previewZoomButtonClasses: {
                prev: "btn btn-navigate",
                next: "btn btn-navigate",
                toggleheader: "btn btn-kv btn-default btn-outline-secondary",
                fullscreen: "btn btn-kv btn-default btn-outline-secondary",
                borderless: "btn btn-kv btn-default btn-outline-secondary",
                close: "btn btn-kv btn-default btn-outline-secondary"
            },
            preferIconicPreview: !1,
            preferIconicZoomPreview: !1,
            allowedPreviewTypes: void 0,
            allowedPreviewMimeTypes: null,
            allowedFileTypes: null,
            allowedFileExtensions: null,
            defaultPreviewContent: null,
            customLayoutTags: {},
            customPreviewTags: {},
            previewFileIcon: '<i class="glyphicon glyphicon-file"></i>',
            previewFileIconClass: "file-other-icon",
            previewFileIconSettings: {},
            previewFileExtSettings: {},
            buttonLabelClass: "hidden-xs",
            browseIcon: '<i class="glyphicon glyphicon-folder-open"></i>&nbsp;',
            browseClass: "btn btn-primary",
            removeIcon: '<i class="glyphicon glyphicon-trash"></i>',
            removeClass: "btn btn-default btn-secondary",
            cancelIcon: '<i class="glyphicon glyphicon-ban-circle"></i>',
            cancelClass: "btn btn-default btn-secondary",
            uploadIcon: '<i class="glyphicon glyphicon-upload"></i>',
            uploadClass: "btn btn-default btn-secondary",
            uploadUrl: null,
            uploadUrlThumb: null,
            uploadAsync: !0,
            uploadExtraData: {},
            zoomModalHeight: 480,
            minImageWidth: null,
            minImageHeight: null,
            maxImageWidth: null,
            maxImageHeight: null,
            resizeImage: !1,
            resizePreference: "width",
            resizeQuality: .92,
            resizeDefaultImageType: "image/jpeg",
            resizeIfSizeMoreThan: 0,
            minFileSize: 0,
            maxFileSize: 0,
            maxFilePreviewSize: 25600,
            minFileCount: 0,
            maxFileCount: 0,
            validateInitialCount: !1,
            msgValidationErrorClass: "text-danger",
            msgValidationErrorIcon: '<i class="glyphicon glyphicon-exclamation-sign"></i> ',
            msgErrorClass: "file-error-message",
            progressThumbClass: "progress-bar bg-success progress-bar-success progress-bar-striped active",
            progressClass: "progress-bar bg-success progress-bar-success progress-bar-striped active",
            progressCompleteClass: "progress-bar bg-success progress-bar-success",
            progressErrorClass: "progress-bar bg-danger progress-bar-danger",
            progressUploadThreshold: 99,
            previewFileType: "image",
            elCaptionContainer: null,
            elCaptionText: null,
            elPreviewContainer: null,
            elPreviewImage: null,
            elPreviewStatus: null,
            elErrorContainer: null,
            errorCloseButton: '<button type="button" class="close kv-error-close">&times;</button>',
            slugCallback: null,
            dropZoneEnabled: !0,
            dropZoneTitleClass: "file-drop-zone-title",
            fileActionSettings: {},
            otherActionButtons: "",
            textEncoding: "UTF-8",
            ajaxSettings: {},
            ajaxDeleteSettings: {},
            showAjaxErrorDetails: !0,
            mergeAjaxCallbacks: !1,
            mergeAjaxDeleteCallbacks: !1,
            retryErrorUploads: !0
        },
        e.fn.fileinputLocales.en = {
            fileSingle: "file",
            filePlural: "files",
            browseLabel: "Browse &hellip;",
            removeLabel: "Remove",
            removeTitle: "Clear selected files",
            cancelLabel: "Cancel",
            cancelTitle: "Abort ongoing upload",
            uploadLabel: "Upload",
            uploadTitle: "Upload selected files",
            msgNo: "No",
            msgNoFilesSelected: "No files selected",
            msgCancelled: "Cancelled",
            msgPlaceholder: "Select {files}...",
            msgZoomModalHeading: "Detailed Preview",
            msgFileRequired: "You must select a file to upload.",
            msgSizeTooSmall: 'File "{name}" (<b>{size} KB</b>) is too small and must be larger than <b>{minSize} KB</b>.',
            msgSizeTooLarge: 'File "{name}" (<b>{size} KB</b>) exceeds maximum allowed upload size of <b>{maxSize} KB</b>.',
            msgFilesTooLess: "You must select at least <b>{n}</b> {files} to upload.",
            msgFilesTooMany: "Number of files selected for upload <b>({n})</b> exceeds maximum allowed limit of <b>{m}</b>.",
            msgFileNotFound: 'File "{name}" not found!',
            msgFileSecured: 'Security restrictions prevent reading the file "{name}".',
            msgFileNotReadable: 'File "{name}" is not readable.',
            msgFilePreviewAborted: 'File preview aborted for "{name}".',
            msgFilePreviewError: 'An error occurred while reading the file "{name}".',
            msgInvalidFileName: 'Invalid or unsupported characters in file name "{name}".',
            msgInvalidFileType: 'Invalid type for file "{name}". Only "{types}" files are supported.',
            msgInvalidFileExtension: 'Invalid extension for file "{name}". Only "{extensions}" files are supported.',
            msgFileTypes: {
                image: "image",
                html: "HTML",
                text: "text",
                video: "video",
                audio: "audio",
                flash: "flash",
                pdf: "PDF",
                object: "object"
            },
            msgUploadAborted: "The file upload was aborted",
            msgUploadThreshold: "Processing...",
            msgUploadBegin: "Initializing...",
            msgUploadEnd: "Done",
            msgUploadEmpty: "No valid data available for upload.",
            msgUploadError: "Error",
            msgValidationError: "Validation Error",
            msgLoading: "Loading file {index} of {files} &hellip;",
            msgProgress: "Loading file {index} of {files} - {name} - {percent}% completed.",
            msgSelected: "{n} {files} selected",
            msgFoldersNotAllowed: "Drag & drop files only! {n} folder(s) dropped were skipped.",
            msgImageWidthSmall: 'Width of image file "{name}" must be at least {size} px.',
            msgImageHeightSmall: 'Height of image file "{name}" must be at least {size} px.',
            msgImageWidthLarge: 'Width of image file "{name}" cannot exceed {size} px.',
            msgImageHeightLarge: 'Height of image file "{name}" cannot exceed {size} px.',
            msgImageResizeError: "Could not get the image dimensions to resize.",
            msgImageResizeException: "Error while resizing the image.<pre>{errors}</pre>",
            msgAjaxError: "Something went wrong with the {operation} operation. Please try again later!",
            msgAjaxProgressError: "{operation} failed",
            ajaxOperations: {
                deleteThumb: "file delete",
                uploadThumb: "file upload",
                uploadBatch: "batch file upload",
                uploadExtra: "form data upload"
            },
            dropZoneTitle: "Drag & drop files here &hellip;",
            dropZoneClickTitle: "<br>(or click to select {files})",
            previewZoomButtonTitles: {
                prev: "View previous file",
                next: "View next file",
                toggleheader: "Toggle header",
                fullscreen: "Toggle full screen",
                borderless: "Toggle borderless mode",
                close: "Close detailed preview"
            }
        },
        e.fn.fileinput.Constructor = i,
        e(document).ready(function () {
            var t = e("input.file[type=file]");
            t.length && t.fileinput()
        })
});