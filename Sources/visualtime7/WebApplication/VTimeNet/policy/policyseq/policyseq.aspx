<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%Response.Expires = -1
Response.CacheControl = "private"
%>
<HTML>
<HEAD>
	<TITLE>Secuencia de Cartera</TITLE>
</HEAD>
<FRAMESET COLS="150,*">
	<FRAME NAME="SEQUENCE" NORESIZE TARGET="FRAFOLDER" SRC="Sequence.aspx" SCROLLING="NO">
	<%
'+ Se carga la página con dos Frames si se está colocando los datos de la póliza
'+ Se carga la página con tres Frames si se va a mostrar la secuencia 
If Request.QueryString.Item("TypePage") = "WithSequence" Then
	With Response
		.Write("<FRAMESET ROWS=""25%,*"">")
		.Write("	<FRAME NAME=""FRAHEADER"" SRC=""Sequence.aspx"" TARGET=""FRAFOLDER"" NORESIZE>")
		.Write("	<FRAME NAME=""FRAFOLDER"" SRC=""CA001_K.aspx"" NORESIZE>")
		.Write("</FRAMESET>")
	End With
Else
	With Response
		.Write("<FRAMESET>")
		.Write("	<FRAME NAME=""FRAFOLDER"" SRC=""CA001_K.aspx"" NORESIZE>")
		.Write("</FRAMESET>")
	End With
End If
%>
	<NOFRAMES>
		<BODY>
			<P>ESTA PÁGINA USA MARCOS, PERO SU EXPLORADOR NO LOS ADMITE.</P>
		</BODY>
	</NOFRAMES>
</FRAMESET>
</HTML>





