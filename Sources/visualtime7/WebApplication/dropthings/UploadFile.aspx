<%@ Page Language="VB" AutoEventWireup="true" CodeFile="UploadFile.aspx.vb" Inherits="dropthings_UploadFile" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <dx:ASPxUploadControl ID="UploadFiles" runat="server" ClientInstanceName="UploadFiles"
                Width="300px" ToolTip="Browse & upload files" ShowProgressPanel="True" meta:resourcekey="UploadFilesResource"
                NullText="Click aquí para buscar archivos..." ShowClearFileSelectionButton="False">
                <ClientSideEvents FileUploadComplete="function(s, e) {
    window.parent.SetUploadFile(e.callbackData, editorClientId, imageClientId, removeClientId);
}"
                    TextChanged="function(s, e) {
	s.Upload();
}" />
                <BrowseButton Image-Url="~/images/dropthings/folder.png" Text="  Buscar" ImagePosition="Left">
                    <Image Url="~/images/dropthings/folder.png">
                    </Image>
                </BrowseButton>
            </dx:ASPxUploadControl>
        </div>
    </form>
</body>
</html>
