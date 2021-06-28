<%@ Page Title="" Language="C#" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="true" CodeFile="MailBox.aspx.cs" Inherits="fasi_SMC_MailBox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/fasi/assets/gridstack/dist/gridstack.min.css" rel="stylesheet" />
    <link href="/fasi/assets/css/summernote.css" rel="stylesheet" />
    <link href="/fasi/assets/css/summernote-bs3.css" rel="stylesheet" />

    <link href="/fasi/assets/jstree/dist/themes/default/style.min.css" rel="stylesheet" />
    <link href="/fasi/assets/css/icheckcustom.css" rel="stylesheet" />
    <link href="/fasi/app/css/default.css?rel=1526062220926" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <div class="row">
        <div class="col-lg-3">
            <div class="ibox float-e-margins">
                <div class="ibox-content mailbox-content">
                    <div class="file-manager">
                        <a class="btn btn-block btn-primary compose-mail" href="MailCompose.aspx" data-i18n="app.form.NewMessageCaption">New Message</a>
                        <div class="space-25"></div>
                        <h5 data-i18n="app.form.FoldersCaption">Folders</h5>
                        <ul class="folder-list m-b-md" style="padding: 0">
                            <li id="inboxlnk"><a href="MailBox.aspx" data-i18n="[html]app.form.InboxCaption"></a></li>
                            <li id="sendedlnk"><a href="MailBox.aspx" data-i18n="[html]app.form.SendMailCaption"></a></li>
                            <li id="draftlnk"><a href="MailBox.aspx" data-i18n="[html]app.form.DraftBoxCaption"></a></li>
                            <li id="deletedlnk"><a href="MailBox.aspx" data-i18n="[html]app.form.TrashBoxCaption"></a></li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-9 animated fadeInRight">
            <div id="MailBoxView">
                <div class="mail-box-header">

                    <form method="get" class="pull-right mail-search">
                        <div class="input-group">
                            <input type="text" id="search" class="form-control input-sm" name="search" data-i18n="[placeholder]app.form.SearchInput">
                            <div class="input-group-btn">
                                <button onclick="SearchMessage()" type="button" class="btn btn-sm btn-primary" data-i18n="app.form.BtnSearch" >
                                    Search
                                </button>
                            </div>
                        </div>
                    </form>
                    <h2 id="BoxCurrentFolder"></h2>
                    <div class="mail-tools tooltip-demo m-t-md">
                        <%--<div class="btn-group pull-right">
                            <button class="btn btn-white btn-sm"><i class="fa fa-arrow-left"></i></button>
                            <button class="btn btn-white btn-sm"><i class="fa fa-arrow-right"></i></button>
                        </div>--%>
                        <button id="Refresh" class="btn btn-white btn-sm" data-toggle="tooltip" data-placement="left" data-i18n="[data-original-title]app.form.BtnRefresh_Title;[html]app.form.BtnRefresh"></button>
                        <button onclick="MarkRead()" class="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top" data-i18n="[data-original-title]app.form.BtnMarkRead_Title"><i class="fa fa-eye"></i></button>
                        <button onclick="MarkImportant()" class="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top" data-i18n="[data-original-title]app.form.BtnMarkImportant_Title"><i class="fa fa-exclamation"></i></button>
                        <button onclick="MoveTrash()" class="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top" data-i18n="[data-original-title]app.form.BtnMoveToTrash_Title"><i class="fa fa-trash-o"></i></button>
                    </div>
                </div>
                <div class="mail-box">

                    <table class="table table-hover table-mail">
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
            <div id="DetailView" style="display: none">
                <div class="mail-box-header">
                    <div class="pull-right tooltip-demo">
                        <a onclick="RFMail(true)" class="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top" data-i18n="[html]app.form.BtnReply_Title">Reply</a>
                        <%--<a href="#" class="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top" title="Print email"><i class="fa fa-print"></i></a>--%>
                        <a onclick="Trash()" class="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top" data-i18n="[data-original-title]app.form.BtnTrash_ToolTip"><i class="fa fa-trash-o"></i></a>
                    </div>
                    <h2 data-i18n="app.form.DetailView_Title">View Message</h2>
                    <div class="mail-tools tooltip-demo m-t-md">
                        <h3>
                            <span id="MESSAGEID" hidden></span>
                            <span id="SENDERID" hidden></span>
                            <span class="font-normal" data-i18n="app.form.Subject_Title">Subject: </span><span id="SUBJECT"></span>
                        </h3>
                        <h5>
                            <span class="pull-right font-normal"><span id="RECEIVED"></span></span>
                            <span class="font-normal" data-i18n="app.form.From_Title">From: </span><span id="SENDERNAME"></span>
                        </h5>
                    </div>
                </div>
                <div class="mail-box">

                    <div class="mail-body">
                        <span id="BODY"></span>
                    </div>

                    <div class="mail-body text-right tooltip-demo">
                        <a onclick="RFMail(true)" class="btn btn-sm btn-white" data-i18n="[html]app.form.BtnReply_Title">Reply</a>
                        <a onclick="RFMail(false)" class="btn btn-sm btn-white" data-i18n="[html]app.form.BtnForward_Title">Forward</a>
                        <%--<button title="" data-placement="top" data-toggle="tooltip" type="button" data-original-title="Print" class="btn btn-sm btn-white"><i class="fa fa-print"></i>Print</button>--%>
                        <button onclick="Trash()" title="" data-placement="top" data-toggle="tooltip" data-i18n="[data-original-title]app.form.BtnTrash_ToolTip;[html]app.form.BtnTrash_Title" class="btn btn-sm btn-white">Remove</button>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div id="ComposeView" style="display: none">
                <div class="mail-box-header">
                    <div class="pull-right tooltip-demo">
                        <a onclick="SendMail(1)" class="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top" data-i18n="[data-original-title]app.form.BtnDraft_ToolTip;[html]app.form.BtnDraft_Title">Draft</a>
                        <a onclick="Discard()" class="btn btn-danger btn-sm" data-toggle="tooltip" data-placement="top" data-i18n="[data-original-title]app.form.BtnDiscard_ToolTip;[html]app.form.BtnDiscard_Title">Discard</a>
                    </div>
                    <h2 data-i18n="app.form.ComposeView_Title">Compose Message</h2>
                </div>
                <div class="mail-box">

                    <div class="mail-body">
                        <input type="hidden" id="DRAFTID" />
                        <form class="form-horizontal" method="get">
                            <div class="form-group">
                                <label class="col-sm-2 control-label" data-i18n="app.form.To_Title">To:</label>

                                <div class="col-sm-10">
                                    <select id="RECEIVER" class="form-control" multiple="multiple" style="width:100%">
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label" data-i18n="app.form.Subject_Title">Subject:</label>

                                <div class="col-sm-10">
                                    <input id="SUBJECTCONTENT" type="text" class="form-control" value="">
                                </div>
                            </div>
                        </form>
                    </div>

                    <div class="mail-text h-200">

                        <div id="BODYCONTENT" class="summernote" style="display: none;">
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="mail-body text-right tooltip-demo">
                        <a id="Send" href="MailBox.aspx" class="btn btn-sm btn-primary" data-toggle="tooltip" data-placement="top" data-i18n="[data-original-title]app.form.BtnSend_ToolTip;[html]app.form.BtnSend_Title">Send</a>
                        <a onclick="Discard()" class="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top" data-i18n="[data-original-title]app.form.BtnDiscard_ToolTip;[html]app.form.BtnDiscard_Title">Discard</a>
                        <a onclick="SendMail(1)" class="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top" data-i18n="[data-original-title]app.form.BtnDraft_ToolTip;[html]app.form.BtnDraft_Title">Draft</a>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">

    <script src="/fasi/assets/js/lodash.min.js"></script>
    <script src="/fasi/assets/gridstack/dist/gridstack.min.js"></script>
    <script src="/fasi/assets/gridstack/dist/gridstack.jQueryUI.min.js"></script>
    <script src="/fasi/assets/jstree/dist/jstree.min.js"></script>
    <script src="/fasi/assets/js/summernote.js"></script>
    <script src="/fasi/assets/js/icheck.min.js"></script>
    <script src='/fasi/assets/js/i18next.min.js'></script>
    <script src="/fasi/app/js/default.js?rel=1526062220926"></script>

    <script src="/fasi/app/js/core.js?rel=1526062220926"></script>
    <script src="MailBox.js"></script>
</asp:Content>
