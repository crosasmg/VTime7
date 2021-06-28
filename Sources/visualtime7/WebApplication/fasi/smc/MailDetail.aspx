﻿<%@ Page Title="" Language="C#" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="true" CodeFile="MailDetail.aspx.cs" Inherits="fasi_SMC_MailDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/fasi/assets/gridstack/dist/gridstack.min.css" rel="stylesheet" />
    <link href="/fasi/assets/jstree/dist/themes/default/style.min.css" rel="stylesheet" />

    <link href="/fasi/app/css/default.css?rel=1526062220926" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <div class="row">
        <div class="col-lg-3">
            <div class="ibox float-e-margins">
                <div class="ibox-content mailbox-content">
                    <div class="file-manager">
                        <a class="btn btn-block btn-primary compose-mail" href="MailCompose.aspx">Compose Mail</a>
                        <div class="space-25"></div>
                        <h5>Folders</h5>
                        <ul class="folder-list m-b-md" style="padding: 0">
                            <li><a href="MailBox.aspx"><i class="fa fa-inbox "></i>Inbox <span class="label label-warning pull-right">16</span> </a></li>
                            <li><a href="MailBox.aspx"><i class="fa fa-envelope-o"></i>Send Mail</a></li>
                            <li><a href="MailBox.aspx"><i class="fa fa-certificate"></i>Important</a></li>
                            <li><a href="MailBox.aspx"><i class="fa fa-file-text-o"></i>Drafts <span class="label label-danger pull-right">2</span></a></li>
                            <li><a href="MailBox.aspx"><i class="fa fa-trash-o"></i>Trash</a></li>
                        </ul>
                        <h5>Categories</h5>
                        <ul class="category-list" style="padding: 0">
                            <li><a href="#"><i class="fa fa-circle text-navy"></i>Work </a></li>
                            <li><a href="#"><i class="fa fa-circle text-danger"></i>Documents</a></li>
                            <li><a href="#"><i class="fa fa-circle text-primary"></i>Social</a></li>
                            <li><a href="#"><i class="fa fa-circle text-info"></i>Advertising</a></li>
                            <li><a href="#"><i class="fa fa-circle text-warning"></i>Clients</a></li>
                        </ul>

                        <h5 class="tag-title">Labels</h5>
                        <ul class="tag-list" style="padding: 0">
                            <li><a href=""><i class="fa fa-tag"></i>Family</a></li>
                            <li><a href=""><i class="fa fa-tag"></i>Work</a></li>
                            <li><a href=""><i class="fa fa-tag"></i>Home</a></li>
                            <li><a href=""><i class="fa fa-tag"></i>Children</a></li>
                            <li><a href=""><i class="fa fa-tag"></i>Holidays</a></li>
                            <li><a href=""><i class="fa fa-tag"></i>Music</a></li>
                            <li><a href=""><i class="fa fa-tag"></i>Photography</a></li>
                            <li><a href=""><i class="fa fa-tag"></i>Film</a></li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-9 animated fadeInRight">
            <div class="mail-box-header">
                <div class="pull-right tooltip-demo">
                    <a href="MailCompose.aspx" class="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top" title="Reply"><i class="fa fa-reply"></i>Reply</a>
                    <a href="#" class="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top" title="Print email"><i class="fa fa-print"></i></a>
                    <a href="MailBox.aspx" class="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top" title="Move to trash"><i class="fa fa-trash-o"></i></a>
                </div>
                <h2>View Message
                </h2>
                <div class="mail-tools tooltip-demo m-t-md">

                    <h3>
                        <span class="font-normal">Subject: </span>Aldus PageMaker including versions of Lorem Ipsum.
                    </h3>
                    <h5>
                        <span class="pull-right font-normal">10:15AM 02 FEB 2014</span>
                        <span class="font-normal">From: </span>alex.smith@corporation.com
                    </h5>
                </div>
            </div>
            <div class="mail-box">

                <div class="mail-body">
                    <p>
                        Hello Jonathan!
                        <br>
                        <br>
                        Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer
                        took a galley of type and scrambled it to make a type <strong>specimen book.</strong>It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. It has survived not only five centuries, but also the leap into electronic typesetting, remaining
                        essentially unchanged.
                    </p>
                    <p>
                        It was popularised in the 1960s with the release <a href="#" class="text-navy">Letraset sheets</a>  containing Lorem Ipsum passages, and more recently with desktop publishing software
                        like Aldus PageMaker including versions of Lorem Ipsum.
                    </p>
                    <p>
                        There are many variations of passages of <strong>Lorem Ipsum</strong>Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of.
                    </p>
                </div>
                <div class="mail-attachment">
                    <p>
                        <span><i class="fa fa-paperclip"></i>2 attachments - </span>
                        <a href="#">Download all</a>
                        |
                            <a href="#">View all images</a>
                    </p>

                    <div class="attachment">
                        <div class="file-box">
                            <div class="file">
                                <a href="#">
                                    <span class="corner"></span>

                                    <div class="icon">
                                        <i class="fa fa-file"></i>
                                    </div>
                                    <div class="file-name">
                                        Document_2014.doc
                                            <br>
                                        <small>Added: Jan 11, 2014</small>
                                    </div>
                                </a>
                            </div>
                        </div>
                        <div class="file-box">
                            <div class="file">
                                <a href="#">
                                    <span class="corner"></span>

                                    <div class="image">
                                        <img alt="image" class="img-responsive" src="img/p1.jpg">
                                    </div>
                                    <div class="file-name">
                                        Italy street.jpg
                                            <br>
                                        <small>Added: Jan 6, 2014</small>
                                    </div>
                                </a>
                            </div>
                        </div>
                        <div class="file-box">
                            <div class="file">
                                <a href="#">
                                    <span class="corner"></span>

                                    <div class="image">
                                        <img alt="image" class="img-responsive" src="img/p2.jpg">
                                    </div>
                                    <div class="file-name">
                                        My feel.png
                                            <br>
                                        <small>Added: Jan 7, 2014</small>
                                    </div>
                                </a>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
                <div class="mail-body text-right tooltip-demo">
                    <a class="btn btn-sm btn-white" href="MailCompose.aspx"><i class="fa fa-reply"></i>Reply</a>
                    <a class="btn btn-sm btn-white" href="MailCompose.aspx"><i class="fa fa-arrow-right"></i>Forward</a>
                    <button title="" data-placement="top" data-toggle="tooltip" type="button" data-original-title="Print" class="btn btn-sm btn-white"><i class="fa fa-print"></i>Print</button>
                    <button title="" data-placement="top" data-toggle="tooltip" data-original-title="Trash" class="btn btn-sm btn-white"><i class="fa fa-trash-o"></i>Remove</button>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">

    <script src="/fasi/assets/js/lodash.min.js"></script>
    <script src="/fasi/assets/gridstack/dist/gridstack.min.js"></script>
    <script src="/fasi/assets/gridstack/dist/gridstack.jQueryUI.min.js"></script>
    <script src="/fasi/assets/jstree/dist/jstree.min.js"></script>

    <script src="/fasi/app/js/default.js?rel=1526062220926"></script>
    <script src="/fasi/widgets/menuVTWidget.js?rel=1526062220926"></script>
    <script src="/fasi/widgets/iFrameWidget.js"></script>

    <script src="MailDetail.js"></script>
</asp:Content>
