<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 13/05/2003 10:35:21 a.m.
</script>
<%    
    Dim objVar
    Dim mstrClass

    If Request.Form("btnSession") = "Crear" Then
        If Request.Form("txtSessionVar") <> vbNullString And _
           Request.Form("txtSessionVal") <> vbNullString Then 
            Session(Request.Form("txtSessionVar"))= Request.Form("txtSessionVal")
        End If
    End If

%>
<HTML>
<HEAD>
<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
<STYLE>
    BODY        {
                    background-color: silver;
                }
    H2          {
                    color: red;
                    font-family:'Verdana';
                    font-size:10pt;
                }
    P           {
                    margin: 10pt
                }
    TH          {
                    border-top: 2 solid navy;
                    border-bottom: 2 solid navy;
                    font-family:'Verdana';
                    font-size:8pt;
                }
    TD          {
                    font-family:'Verdana';
                    font-size:8pt;
                }
	TD.sel		{   
	                background-color:ivory;
	            }
	TD.unsel	{   
	                background-color:lightblue
	            }
</STYLE>
</HEAD>
<BODY>
<H2>Session</H2>

<P>
<FORM ACTION=PutSession.asp METHOD=POST NAME=frmSession>
<TABLE BORDER="0" WIDTH="99%">
<TR><TH WIDTH="30%">Variable</TH><TH>Valor</TH></TR>
<TR><TH><INPUT type="text" name=txtSessionVar></TH>
    <TH><INPUT type="text" name=txtSessionVal>
    <INPUT type="submit" value=Crear name=btnSession></TH>
</TR>
<%
	'
	' Despliege de variables de session
	'
	for each objVar in Session.Contents
		If mstrClass = "SEL" Then
			mstrClass = "UNSEL"
		Else
			mstrClass = "SEL"
		End If

        On error resume next		
		Response.Write("<TR><TD CLASS=" & mstrClass & ">" & objVar & _
						"</TD><TD CLASS=" & mstrClass & ">&nbsp;")
						
        If  (Session(objVar) is Nothing) Then 						
            If Session(objVar) Is Nothing Then 
                Response.Write( "[Objeto]Nothing</TD></TR>")
            Else
                Response.Write("[Objeto]")
                On error resume next
                Response.Write(Session(objVar).Name)
                On error goto 0
                Response.Write("</TD></TR>")
            End If
        Else
            Response.Write(Session(objVar) & "</TD></TR>")
        End If
	
	next 
%>
</TABLE>
</FORM>
</P>

<H2>Application Contents</H2>
<P>
<TABLE BORDER="0" WIDTH="99%">
<TR><TH WIDTH="30%">Variable</TH><TH>Valor</TH></TR>
<%
	'
	' Despliege de variables de application
	'
	for each objVar in	Application.Contents
		If mstrClass = "SEL" Then
			mstrClass = "UNSEL"
		Else
			mstrClass = "SEL"
		End If

		Response.Write( "<TR><TD CLASS=" & mstrClass & ">" & objVar & _
						"</TD><TD CLASS=" & mstrClass & ">&nbsp;" )
		
        If Not (Application(objVar) is Nothing) Then
            If Application(objVar) Is Nothing Then 
                Response.Write("[Objeto]Nothing</TD></TR>")
            Else
                Response.Write("[Objetouu]")
                On error resume next
                Response.Write(Application(objVar).Name)
                On error goto 0
                Response.Write("</TD></TR>")
            End If
        Else
            Response.Write(Application(objVar) & "</TD></TR>")
        End If

	next 
%>
</TABLE>
</P>
<H2>Application Statics</H2>
<P>
<TABLE BORDER="0" WIDTH="99%">
<TR><TH WIDTH="30%">Variable</TH><TH>Valor</TH></TR>
<%

	for each objVar in	Application.StaticObjects
		If mstrClass = "SEL" Then
			mstrClass = "UNSEL"
		Else
			mstrClass = "SEL"
		End If

		Response.Write("<TR><TD CLASS=" & mstrClass & ">" & objVar & _
						"</TD><TD CLASS=" & mstrClass & ">&nbsp;" )
		
        If (objVar is Nothing) Then 
            Response.Write("[Objeto]Nothing</TD></TR>")
        Else
            Response.Write("[Objetooo]")
            On error resume next 
            Response.Write(objVar.Name)
            On error goto 0
            Response.Write("</TD></TR>")
        End If

	next 

%>
</TABLE>
</P>



<H2>ServerVariables</H2>
<P>
<TABLE BORDER="0" WIDTH="99%">
<TR><TH WIDTH="30%">Variable</TH><TH>Valor</TH></TR>
<%

	'
	' Despliege de variables de servidor
	'
	for each objVar in Request.ServerVariables
		If mstrClass = "SEL" Then
			mstrClass = "UNSEL"
		Else
			mstrClass = "SEL"
		End If

        On error resume next		
		Response.Write("<TR><TD CLASS=" & mstrClass & ">" & objVar & _
						"</TD><TD CLASS=" & mstrClass & ">&nbsp;" )
						
        Response.Write(Request.ServerVariables(objVar) & "</TD></TR>")
	
	next 


%>
</TABLE>
</P>

</BODY>
</HTML>