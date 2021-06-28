<script language="VB" runat="Server">
Dim drillu As String
Dim basepage As String
Dim previewu As String
Dim brch As String
Dim previouspage As Short
Dim CurrentPageNumber As String
Dim previewd As String
Dim nextlink As String
Dim drilld As String
Dim lastlink As String
Dim searchFound As String
Dim previouslink As String
Dim nextpage As Short
Dim lastknownpage As String
Dim getPageCommand As String
Dim firstlink As String
Dim messageText As String
Dim LastPageNumber As String

Dim counter As Short
Dim tmpArray As Object
Dim upperBound As Object


</script>
<%
' 05/02/98
' Added the following features:
' Tab Query String Parameter
'	- This is the selected tab's tabArray index value.
' Page Expiry Time
'	-  The page will expire when downloaded by browser so that user is insured that all data
' will be current.
' DrillDown Tabs
'	- Added in the session("wtabArray") object to keep track of the drill down tabs.
' Search
'	- Added javascript window.alert function call to indicate when text is not found in rpt view.
' Goto Page Text Box
'	- Added textbox and filenew.gif so user can enter and request desired page number.
'	NOTE: Netscape 2.0 browsers do not call the on submit event handler when the image is selected.
'   Thus, the user will not be warned when incorrect data is entered into the goto page box.
'   This problem does not happen when the user selects return.
response.Expires = 0
' Viewer Tab images
drilld = "<img border=0 src='/viewer/images/toolbar/pdrilld.gif' alt = 'Grupo principal'>"
drillu = "<img border=0 src='/viewer/images/toolbar/cdrillu.gif' alt = 'Grupo actual'>"
previewu = "<img border=0 src='/viewer/images/toolbar/pviewu.gif' alt = 'Vista'>"
previewd = "<img border=0 src='/viewer/images/toolbar/pviewd.gif' alt = 'Vista'>"
' Set the correct numbers on the paging buttons
brch = request.QueryString.Item("BRCH")
If brch <> "" Then
	brch = "&" & "brch=" & brch
	basepage = "<a href=" & Chr(34) & "javascript:parent.parent.location='rptserver.aspx?init=html_frame&page=1'" & Chr(34) & ">"
	
End If

getPageCommand = "rptserver.aspx?cmd=toolbar%5Fpage&viewer=html%5Fframe&vfmt=html%5Fframe" & brch


searchFound = request.QueryString.Item("SEARCHFOUND")
If searchFound <> "" Then
	If CShort(searchFound) = 0 Then
		messageText = "onLoad = " & Chr(34) & "window.alert('No se encuentra el texto en el informe.');" & Chr(34)
	End If
End If

CurrentPageNumber = CStr(session("wCurrentPageNumber"))
lastknownpage = CStr(session("wlastknownpage"))
LastPageNumber = CStr(session("wLastPageNumber"))

If CurrentPageNumber = "" Then
	CurrentPageNumber = "1"
End If

If lastknownpage = "" Then
	lastknownpage = "0"
End If



If LastPageNumber <> "" And (CurrentPageNumber = LastPageNumber) Then
	lastknownpage = CurrentPageNumber
	' remember the last known page
	session("wlastknownpage") = CurrentPageNumber
	nextlink = ""
	lastlink = ""
	If CShort(CurrentPageNumber) > 1 Then
		previouspage = CShort(CurrentPageNumber) - 1
		previouslink = "<a href=" & Chr(34) & "javascript:parent.location='rptserver.aspx?cmd=toolbar%5Fpage&viewer=html%5Fframe&vfmt=html%5Fframe&page=" & previouspage & brch & "'" & Chr(34) & ">"
		firstlink = "<a href=" & Chr(34) & "javascript:parent.location='rptserver.aspx?cmd=toolbar%5Fpage&viewer=html%5Fframe&vfmt=html%5Fframe&page=1" & brch & "'" & Chr(34) & ">"
	Else
		previouslink = ""
		firstlink = ""
	End If
Else
	If (CShort(lastknownpage) < CShort(CurrentPageNumber)) And LastPageNumber = "" Then
		' remember the last known page
		session("wlastknownpage") = CurrentPageNumber
		lastknownpage = CurrentPageNumber & "+"
	Else
		If lastknownpage <> LastPageNumber Then
			lastknownpage = lastknownpage & "+"
		End If
	End If
	If CShort(CurrentPageNumber) > 1 Then
		previouspage = CShort(CurrentPageNumber) - 1
		previouslink = "<a href=" & Chr(34) & "javascript:parent.location='rptserver.aspx?cmd=toolbar%5Fpage&viewer=html%5Fframe&vfmt=html%5Fframe&page=" & previouspage & brch & "'" & Chr(34) & ">"
		firstlink = "<a href=" & Chr(34) & "javascript:parent.location='rptserver.aspx?cmd=toolbar%5Fpage&viewer=html%5Fframe&vfmt=html%5Fframe&page=1" & brch & "'" & Chr(34) & ">"
	Else
		previouslink = ""
		firstlink = ""
		previouspage = 1
	End If
	nextpage = CShort(CurrentPageNumber) + 1
	nextlink = "<a href=" & Chr(34) & "javascript:parent.location='rptserver.aspx?cmd=toolbar%5Fpage&viewer=html%5Fframe&vfmt=html%5Fframe&page=" & nextpage & brch & "'" & Chr(34) & ">"
	lastlink = "<a href=" & Chr(34) & "javascript:parent.location='rptserver.aspx?cmd=toolbar%5Fpage&viewer=html%5Fframe&vfmt=html%5Fframe&page=32756" & brch & "'" & Chr(34) & ">"
End If

%>


<html>
<script language="javascript">

function ValidateNumber(val, msg)
{

  if (val == "")
  {
    alert("Introduzca un valor por el campo " + msg);
    return (false);
  }

  var checkOK = "0123456789";
  var checkStr = val;
  var allValid = true;
  var decPoints = 0;
  var allNum = "";
  for (i = 0;  i < checkStr.length;  i++)
  {
    ch = checkStr.charAt(i);
    for (j = 0;  j < checkOK.length;  j++)
      if (ch == checkOK.charAt(j))
        break;
    if (j == checkOK.length)
    {
      allValid = false;
      break;
    }
    allNum += ch;
  }
  if (!allValid)
  {
    alert("Introduzca solamente caracteres dígitos en el campo " + msg);
    return (false);
  }

  var chkVal = allNum;
  var prsVal = parseInt(allNum);
  if (chkVal != "" && !(prsVal >= "1"))
  {
    alert(" Introduzca un valor mayor que \"0\" en el campo " + msg);
    return (false);
  }
  return (true);
}


var currentValue = "<%response.Write(CurrentPageNumber)%>";

function checkValue(){

	var pageNumber = document.forms[0].elements[0].value;
	if(!ValidateNumber(pageNumber, "Goto Page Number")){
		document.forms[0].elements[0].value = currentValue;
		parent.status = "Introduzca un valor numérico positivo. Sin espacios.";
		return false;
		}
	else
		// a new page will be downloaded with the next page number
		return true;

}

</script>
<body background="/viewer/images/toolbar/toolbg.gif" topmargin="0" leftmargin="0" <%response.Write(messageText)%>>
<form method="POST" name="getPg" target="CrystalViewerPageFrame" action="<%response.Write(getPageCommand)%>" onSubmit="return checkValue();">
<table border="0" width="100%" cellpadding="0" cellspacing="0" height="100%"><tr nowrap>
<td nowrap align="right" width="10%"><%response.Write(firstlink)%><img border="0" src="/viewer/images/toolbar/first.gif" alt="Primera página"><%response.Write(previouslink)%><img border="0" src="/viewer/images/toolbar/prev.gif" alt="Página previa"></td>
<td nowrap valign="center" align="center" width="10%"> <b> <%response.Write(CurrentPageNumber)%> </b> de <%response.Write(lastknownpage)%></td>
<td nowrap align="left" width="10%"><%response.Write(nextlink)%><img border="0" src="/viewer/images/toolbar/next.gif" alt="Próxima página"></a><%response.Write(lastlink)%><img border="0" src="/viewer/images/toolbar/last.gif" alt="Ultima página"></a></td>
<td align="left" width="5%"><input type="text" value="<%response.Write(CurrentPageNumber)%>" size="4" maxlength="5" name="PAGE" alt="Ir a página"></td>
<td align="left" width="5%"> <input type="image" src="/viewer/images/toolbar/filenew.gif" alt="Ir a página"></td>
</form>
<form method="POST" name="pf" target="CrystalViewerPageFrame" action="rptserver.aspx?cmd=srch&amp;viewer=html%5Fframe&amp;vfmt=html_frame&amp;page=<%response.Write(CurrentPageNumber)%>&amp;dir=FOR&amp;case=0<%response.Write(brch)%>">
<td nowrap align="center" width="15%"><a href="javascript:parent.parent.location='rptserver.aspx?cmd=rfsh&amp;viewer=html%5Fframe&amp;vfmt=html%5Fframe&amp;page=<%response.Write(CurrentPageNumber)%>'"><img border="0" src="/viewer/images/toolbar/refresh.gif" alt="Actualizar"></a></td>
<td align="right" width="15%"><input type="text" size="10" maxlength="255" name="text"></td>
<td align="left" width="5%"><input type="image" src="/viewer/images/toolbar/search.gif" alt="Buscar texto"></td>
<td nowrap valign="bottom" align="right" width="20%">
<%tmpArray = session("wtabArray")
counter = Int(UBound(tmpArray) / 5)
If tmpArray(0) <> "EMPTY" Then
	response.Write(drillu)
	If counter > 0 Then
		response.Write("<a href=" & Chr(34) & "javascript:parent.parent.location = 'htmstart.aspx?tab=" & (counter * 5) & "'" & Chr(34) & ">")
		response.Write(drilld & "</a>")
	End If
	response.Write("<a href=" & Chr(34) & "javascript:parent.parent.location = 'htmstart.aspx?tab=" & 0 & "'" & Chr(34) & ">")
	response.Write(previewd & "</a>")
Else
	response.Write(previewu)
End If%>
</td>
</tr></table>
</form>
</body>
</html>





