<%@ Page Language="VB" AutoEventWireup="false" CodeFile="fm.aspx.vb" Inherits="Support_fm" %>

<!DOCTYPE HTML>
<html>
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/scripts/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
           
                fm.SetHeight($(window).height());
            
        });
        $(window).resize(function () {
            
                fm.SetHeight($(window).height());
            
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxFileManager ID="ASPxFileManager1" ClientVisible="false"  runat="server" CssFilePath="~/App_Themes/DevEx/{0}/styles.css"
            CssPostfix="DevEx" ClientInstanceName="fm">
            <Styles CssFilePath="~/App_Themes/DevEx/{0}/styles.css" CssPostfix="DevEx">
            </Styles>
            <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" />
            <SettingsEditing AllowCreate="True" AllowDelete="True" AllowMove="True" AllowRename="True" />
            <SettingsToolbar ShowDownloadButton="True" />
            <SettingsUpload UseAdvancedUploadMode="True">
                <AdvancedModeSettings EnableMultiSelect="True" />
            </SettingsUpload>
            <Images SpriteCssFilePath="~/App_Themes/DevEx/{0}/sprite.css">
                <FolderContainerNodeLoadingPanel Url="~/App_Themes/DevEx/Web/tvNodeLoading.gif">
                </FolderContainerNodeLoadingPanel>
                <LoadingPanel Url="~/App_Themes/DevEx/Web/Loading.gif">
                </LoadingPanel>
            </Images>
        </dx:ASPxFileManager>
    </div>
    </form>
</body>
</html>