<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLetter" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:49:58 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mstrPath As String

'% insDefineHeader
'----------------------------------------
Private Sub insDefineHeader()
	'----------------------------------------        
	mobjGrid = New eFunctions.Grid
	
	'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:49:58 a.m.
	mobjGrid.sSessionID = Session.SessionID
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "Variables"
	
	With mobjGrid
		.Codispl = "LT001"
		.Codisp = "LT001_K"
		.Width = 500
		.Height = 250
		.AddButton = False
		.DeleteButton = False
	End With
	
	With mobjGrid.Columns
		.AddCheckColumn(104458, "", "chkSel", "",  ,  , "insSelVar(this)")
		Call .AddTextColumn(7283,"Group", "tctGroup", 30, String.Empty,,vbNullString)
		.AddTextColumn(7284,"Variable", "tctVar", 12, "",,vbNullString)
		.AddTextColumn(7285,"Description", "tctDescript", 30, "",,vbNullString)
	End With
	mobjGrid.Columns("Sel").GridVisible = False
End Sub

'%insGetVariables
'-----------------------------------
Private Function insGetVariables() As Object
	'-----------------------------------    
	Dim lcolVariables As eLetter.GroupVariabless
	Dim lobjVariable As eLetter.GroupVariables
	
	lcolVariables = New eLetter.GroupVariabless
	lobjVariable = New eLetter.GroupVariables
	
	If lcolVariables.Find(-32768, -32768) Then
		For	Each lobjVariable In lcolVariables
			With mobjGrid
				.Columns("chkSel").onClick = "insSelVar(this,""" & lobjVariable.sVariable & """)"
				.Columns("tctGroup").DefValue = lobjVariable.sGroupDescript
				.Columns("tctVar").DefValue = lobjVariable.sVariable
				.Columns("tctDescript").DefValue = lobjVariable.sDescript
				Response.Write(.DoRow)
			End With
		Next lobjVariable
	End If
	Response.Write(mobjGrid.closeTable)
	lobjVariable = Nothing
	lcolVariables = Nothing
End Function


'% insOpenDocument
'-------------------------------------
Private Function insOpenDocument() As Object
	'-------------------------------------
	Dim lobjLetter As eLetter.Letter
	Dim lobjLettAcusse As eLetter.LettAccuse
	Dim lstrNameCorresp As String
	Dim lstrLetter As String
	
	Dim lrecrea_Letter As eRemoteDB.VisualTimeConfig

	lobjLetter = New eLetter.Letter
	
	lrecrea_Letter = New eRemoteDB.VisualTimeConfig
        mstrPath = Replace(lrecrea_Letter.LoadSetting("Correspondence","C:\Model of correspondence", "Paths"),"\","\\")

	With lobjLetter
		If Request.QueryString.Item("Location") <> String.Empty And Request.QueryString.Item("sBtn") = "2" Then
			
		    Response.Write(mobjValues.TextAreaControl("tctLetter", 2, 2, ""))
		End If
		
		Response.Write("<SCRIPT>")
		Response.Write("var mstrFileName;")
		Response.Write("var mobjFS;")
		Response.Write("var mobjFile;")
		
		Response.Write("var lstrPath = '" & Replace(mstrPath,"\","\\") & "';")
		
		Response.Write("mobjFS = new ActiveXObject('Scripting.FileSystemObject');")
		Response.Write("	if (mobjFS.FolderExists('" & mstrPath & "') == false)")
		Response.Write("	{")
		Response.Write("		mobjFS.CreateFolder('" & mstrPath & "');")
		Response.Write("	}")
		If Request.QueryString.Item("nLetterNum") <> String.Empty And Request.QueryString.Item("nLanguage") <> String.Empty Then
			If Request.QueryString.Item("Location") = String.Empty Then
				lstrNameCorresp = Request.QueryString.Item("nLetterNum") & Request.QueryString.Item("nLanguage")
				Response.Write("mstrFileName = '" & mstrPath & "\\" & lstrNameCorresp & ".tmp.rtf';")
			    'Response.Write("self.document.forms[0].tctLetter.value= '';")
			Else
				lstrNameCorresp = Request.QueryString.Item("Location")
				Response.Write("mstrFileName = '" & Replace(lstrNameCorresp, "\", "\\") & "';")
				
				Response.Write("var lstrText; var objFile; var objFSO;")
				Response.Write(" objFSO = new ActiveXObject('Scripting.FileSystemObject');")
				Response.Write(" objFile = objFSO.OpenTextFile('" & Replace(lstrNameCorresp, "\", "\\") & "',1);")
				Response.Write(" if (!objFile.AtEndOfStream )")
				Response.Write("     lstrText = objFile.ReadAll();")
				Response.Write(" objFile.Close(); ")
				Response.Write("self.document.forms[0].tctLetter.value= lstrText;")
			End If
		Else
		
			Response.Write("mstrFileName = '" & mstrPath & "\\' + mobjFS.GetTempName() + '.rtf';")
		End If
		
		Response.Write("mobjFS.CreateTextFile(mstrFileName,true);")
		
		Response.Write("mobjFile = mobjFS.OpenTextFile(mstrFileName, 8, true);")
		
		If Request.QueryString.Item("Location") <> String.Empty And Request.QueryString.Item("sBtn") = "2" Then
			Response.Write(" mobjFile.write (document.forms[0].tctLetter.value);mobjFile.close();insLoadWord(2);window.close();")
			'Response.Write(" mobjFile.close();insLoadWord(2);window.close();")
			Response.Write("</" & "Script>")
		Else
			Response.Write("</" & "Script>")
			If Request.QueryString.Item("nLettRequest") <> Nothing OR Request.QueryString.Item("nLettRequest") <> ""  Then
				lobjLettAcusse = New eLetter.LettAccuse
				
				If lobjLettAcusse.Find(CShort(Request.QueryString.Item("nLettRequest")), Request.QueryString.Item("nClientReg")) Then
					lstrLetter = lobjLettAcusse.tletter
				End If
				
				lobjLettAcusse = Nothing
			End If
			
			If lstrLetter = String.Empty Then
				
				If .Find(Convert.ToInt16(Request.QueryString.Item("nLetterNum")), Convert.ToInt16(Request.QueryString.Item("nLanguage")), DateTime.Today) Then
Response.Write("" & vbCrLf)
Response.Write("					<DIV Id=hdiv visibility='hide' style=""display:hide"">			" & vbCrLf)
Response.Write("					")

					
					If Request.QueryString.Item("sCustomLetter") <> String.Empty Then
						Response.Write(Replace(mobjValues.TextAreaControl("tctLetter", 2, 2, PreMerge(lobjLetter)), "&nbsp;", "&#032;"))
					Else
					    Response.Write(Replace(mobjValues.TextAreaControl("tctLetter", 2, 2, .tletter), "&nbsp;", "&#032;"))
					End If
					
Response.Write("" & vbCrLf)
Response.Write("					</DIV>" & vbCrLf)
Response.Write("					")

					Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("					ShowDiv('hdiv','hide');" & vbCrLf)
Response.Write("					function insWriteDoc(){" & vbCrLf)
Response.Write("						var lintIndex;" & vbCrLf)
Response.Write("					    mobjFile.write (document.forms[0].tctLetter.value);" & vbCrLf)
Response.Write("				")

				Else
					Response.Write("<SCRIPT>")
Response.Write("" & vbCrLf)
Response.Write("		              function insWriteDoc(){" & vbCrLf)
Response.Write("						mobjFile.WriteLine(""{\\rtf1\\ansi\\ansicpg1252\\uc1 \\deff0\\deflang3082\\deflangfe3082{\\fonttbl{\\f0\\froman\\fcharset0\\fprq2{\\*\\panose 02020603050405020304}Times New Roman;}{\\f30\\froman\\fcharset238\\fprq2 Times New Roman CE;}{\\f31\\froman\\fcharset204\\fprq2 Times New Roman Cyr;}"");" & vbCrLf)
Response.Write("						mobjFile.WriteLine(""{\\f33\\froman\\fcharset161\\fprq2 Times New Roman Greek;}{\\f34\\froman\\fcharset162\\fprq2 Times New Roman Tur;}{\\f35\\froman\\fcharset177\\fprq2 Times New Roman (Hebrew);}{\\f36\\froman\\fcharset178\\fprq2 Times New Roman (Arabic);}"");" & vbCrLf)
Response.Write("						mobjFile.WriteLine(""{\\f37\\froman\\fcharset186\\fprq2 Times New Roman Baltic;}}{\\colortbl;\\red0\\green0\\blue0;\\red0\\green0\\blue255;\\red0\\green255\\blue255;\\red0\\green255\\blue0;\\red255\\green0\\blue255;\\red255\\green0\\blue0;\\red255\\green255\\blue0;\\red255\\green255\\blue255;"");" & vbCrLf)
Response.Write("						mobjFile.WriteLine(""\\red0\\green0\\blue128;\\red0\\green128\\blue128;\\red0\\green128\\blue0;\\red128\\green0\\blue128;\\red128\\green0\\blue0;\\red128\\green128\\blue0;\\red128\\green128\\blue128;\\red192\\green192\\blue192;}{\\stylesheet{"");" & vbCrLf)
Response.Write("						mobjFile.WriteLine(""\\ql \\li0\\ri0\\widctlpar\\aspalpha\\aspnum\\faauto\\adjustright\\rin0\\lin0\\itap0 \\fs24\\lang3082\\langfe3082\\cgrid\\langnp3082\\langfenp3082 \\snext0 Normal;}{\\*\\cs10 \\additive Default Paragraph Font;}}{\\info"");" & vbCrLf)
Response.Write("						mobjFile.WriteLine(""{\\creatim\\yr2001\\mo7\\dy9\\hr23\\min40}{\\revtim\\yr2001\\mo7\\dy9\\hr23\\min40}{\\version2}{\\edmins0}{\\nofpages1}{\\nofwords0}{\\nofchars0}{\\*\\company EASE}{\\nofcharsws0}{\\vern8249}}\\paperw11906\\paperh16838\\margl1701\\margr1701\\margt1417\\margb1417 "");" & vbCrLf)
Response.Write("						mobjFile.WriteLine(""\\deftab708\\widowctrl\\ftnbj\\aenddoc\\hyphhotz425\\noxlattoyen\\expshrtn\\noultrlspc\\dntblnsbdb\\nospaceforul\\hyphcaps0\\formshade\\horzdoc\\dgmargin\\dghspace180\\dgvspace180\\dghorigin1701\\dgvorigin1984\\dghshow1\\dgvshow1"");" & vbCrLf)
Response.Write("						mobjFile.WriteLine(""\\jexpand\\viewkind4\\viewscale100\\pgbrdrhead\\pgbrdrfoot\\splytwnine\\ftnlytwnine\\htmautsp\\nolnhtadjtbl\\useltbaln\\alntblind\\lytcalctblwd\\lyttblrtgr\\lnbrkrule \\fet0\\sectd \\linex0\\headery708\\footery708\\colsx708\\endnhere\\sectlinegrid360\\sectdefaultcl "");" & vbCrLf)
Response.Write("						mobjFile.WriteLine(""{\\*\\pnseclvl1\\pnucrm\\pnstart1\\pnindent720\\pnhang{\\pntxta .}}{\\*\\pnseclvl2\\pnucltr\\pnstart1\\pnindent720\\pnhang{\\pntxta .}}{\\*\\pnseclvl3\\pndec\\pnstart1\\pnindent720\\pnhang{\\pntxta .}}{\\*\\pnseclvl4\\pnlcltr\\pnstart1\\pnindent720\\pnhang{\\pntxta )}}{\\*\\pnseclvl5"");" & vbCrLf)
Response.Write("						mobjFile.WriteLine(""\\pndec\\pnstart1\\pnindent720\\pnhang{\\pntxtb (}{\\pntxta )}}{\\*\\pnseclvl6\\pnlcltr\\pnstart1\\pnindent720\\pnhang{\\pntxtb (}{\\pntxta )}}{\\*\\pnseclvl7\\pnlcrm\\pnstart1\\pnindent720\\pnhang{\\pntxtb (}{\\pntxta )}}{\\*\\pnseclvl8\\pnlcltr\\pnstart1\\pnindent720\\pnhang"");" & vbCrLf)
Response.Write("						mobjFile.WriteLine(""{\\pntxtb (}{\\pntxta )}}{\\*\\pnseclvl9\\pnlcrm\\pnstart1\\pnindent720\\pnhang{\\pntxtb (}{\\pntxta )}}\\pard\\plain \\ql \\li0\\ri0\\widctlpar\\aspalpha\\aspnum\\faauto\\adjustright\\rin0\\lin0\\itap0 \\fs24\\lang3082\\langfe3082\\cgrid\\langnp3082\\langfenp3082 {"");" & vbCrLf)
Response.Write("						mobjFile.WriteLine(""\\par }}"");" & vbCrLf)
Response.Write("		")

				End If
			Else
				
Response.Write("" & vbCrLf)
Response.Write("		          <DIV Id=hdiv visibility='hide' style=""display:hide"">" & vbCrLf)
Response.Write("		")

				Response.Write(Replace(mobjValues.TextAreaControl("tctLetter", 2, 2, lstrLetter), "&nbsp;", "&#032;"))
				Response.Write("<SCRIPT>" & vbCrLf)
				
Response.Write("                ShowDiv('hdiv','hide');" & vbCrLf)
Response.Write("		                    function insWriteDoc(){" & vbCrLf)
Response.Write("		                    var lintIndex;" & vbCrLf)
Response.Write("		                    mobjFile.write (document.forms[0].tctLetter.value);" & vbCrLf)
Response.Write("		")

			End If
Response.Write("" & vbCrLf)
Response.Write("		      mobjFile.close();" & vbCrLf)
Response.Write("		      }      " & vbCrLf)
Response.Write("		</" & "script>      " & vbCrLf)
Response.Write("		")

			
		End If
		
	End With
	
	lobjLetter = Nothing
	lrecrea_Letter = Nothing
End Function

'------------------------------------------
Public Function PreMerge(ByRef lobjLetter As eLetter.Letter) As String
	'------------------------------------------
	With lobjLetter.oParameters
		Select Case Request.QueryString.Item("sCodispl")
			'**+ Clients
			'+ Clientes
			Case "SCA801"
				.Add(Session("sclient"))
				
				'**+ Policies
				'+ Pólizas
			Case "SCA802", "CA034", "CA033", "SCA805"
				.Add(Session("sCertype"))
				.Add(Session("nBranch"))
				.Add(Session("nProduct"))
				.Add(Session("nPolicy"))
				.Add(Session("nCertif"))
				
				'**+ Claims
				'+ Siniestros
			Case "SCA803"
				.Add(Session("nClaim"))
				.Add(Session("nCase_Num"))
				.Add(Session("nDeman_type"))
		End Select
	End With
	
	'Sin Language
	lobjLetter.MergeDocument(Nothing, _
	                         Nothing, _
	                         Session("dEffecdate"), _
	                         Session("nUserCode"), _
	                         False, _
	                         1, _
	                         Request.QueryString.Item("nLetterNum"), _
	                         0, _ 
	                         String.Empty, _
	                         mobjValues.StringToType(Request.QueryString.Item("nLettRequest"), eFunctions.Values.eTypeData.etdInteger), _ 
	                         True)
	                         
    'Con Language
	'lobjLetter.MergeDocument(Nothing, Nothing, Session("dEffecdate"), Session("nUserCode"), False, 1, Request.QueryString.Item("nLetterNum"), Request.QueryString.Item("nLanguage"), String.Empty, CShort(Request.QueryString.Item("nLettRequest")), True)
    'Original
    'lobjLetter.MergeDocument(Nothing, Nothing, Session("dEffecdate"), Session("nUserCode"), False, 1, lobjLetter.nLetterNum,  , String.Empty, CShort(Request.QueryString.Item("nLettRequest")), True)
	
	PreMerge = lobjLetter.sMergeResult
	'lobjLetter = Nothing
End Function

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("Variables")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:49:58 a.m.
mobjValues.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "Variables"
%>
<HTML>
<HEAD>
	

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	
<SCRIPT>
var mstrList = "";
//--------------------------------------------------
function insLoadWord(btn){
//--------------------------------------------------
	var lstrFileName;
	var lobjFS;
	var lobjFile;
    var lobjWord; 
    var oCatch;
    
	lobjFS = new ActiveXObject("Scripting.FileSystemObject");
	
	if (lobjFS.FolderExists(lstrPath) == false)
	{
		lobjFS.CreateFolder(lstrPath);
	}

	lstrFileName = lstrPath + "\\" + lobjFS.GetTempName();
    
	lobjFile = lobjFS.OpenTextFile(lstrFileName, 2, true);

	lobjFile.WriteLine(mstrList);
	lobjFile.WriteLine(mstrList);
	lobjFile.close();
	lobjWord = new ActiveXObject("Word.Application")
    lobjWord.visible = true;

    lobjWord.activate();
		
// Se abre el archivo en modo escritura
	
	if (btn ==2){
		try{lobjWord.Documents.open(mstrFileName)}
		catch(oCatch){}	
		finally{}
	}
//Se abre el archivo en modo lectura	
	else
	{
	    if (typeof(btn)=="undefined"){
	        try{lobjWord.Documents.open(mstrFileName,false,true)}
		    catch(oCatch){}	
		    finally{}
	    }
	    else
	    {
		    try{lobjWord.Documents.open(mstrFileName)}
		    catch(oCatch){}	
		    finally{}
        }
	}

    if (mstrList!=""){
    lobjWord.ActiveDocument.MailMerge.OpenDataSource(lstrFileName, 
//      lobjWord.ActiveDocument.MailMerge.OpenDataSource(mstrFileName, 
                                                     0, false,true,true,false,
                                                     "","",false,"","","","","");
    }

}	        
	</SCRIPT>
</HEAD>
<BODY>
<FORM id=form1 name=form1>    
<%
    If Request.QueryString.Item("sQuery") = "1" Then
        If Request.QueryString.Item("sCodispl") = "SCA801" Then
            Session("sClient") = Request.QueryString.Item("sClient")
        End If
        If Request.QueryString.Item("sCodispl") = "SCA802" Then
            Session("sCertype") = Request.QueryString.Item("sCertype")
            Session("nBranch") = Request.QueryString.Item("nBranch")
            Session("nProduct") = Request.QueryString.Item("nProduct")
            Session("nPolicy") = Request.QueryString.Item("nPolicy")
            Session("nCertif") = Request.QueryString.Item("nCertif")
            Session("dEffecdate") = Request.QueryString.Item("dEffecdate")
        End If
        If Request.QueryString.Item("sCodispl") = "SCA803" Then
            Session("nClaim") = Request.QueryString.Item("nClaim")
            Session("nCase_num") = Request.QueryString.Item("nCase_num")
            Session("nDeman_type") = Request.QueryString.Item("nDeman_type")
        End If
    End If
    insOpenDocument()

    If Request.QueryString.Item("Type") = "upd" And Request.QueryString.Item("sCustomLetter") <> "1" Then
        Response.Write(mobjValues.StyleSheet)%>
		<SCRIPT>
			//--------------------------------------------------
			function insSelVar(oObject, sValue) {
			//--------------------------------------------------
			   
			    if (oObject.checked)
			    {
					if(mstrList == "")
						mstrList +=  sValue;
					else
						mstrList +=  "," + sValue;
			    }
			    else
			    {
					if(mstrList.search(",") == -1)
						mstrList = "";
					else
						if(mstrList.search(sValue) == 0)
							mstrList = mstrList.replace(sValue+",","");
						else
							mstrList = mstrList.replace(","+sValue,"");
			    }
			}
			//--------------------------------------------------
			function insAccept(btn) {
			//--------------------------------------------------
				insWriteDoc(); 
				insLoadWord(btn);         
				window.close();
			}

			//--------------------------------------------------
			function insCancel() {
			//--------------------------------------------------
			    window.close();
			}
			</SCRIPT>        
       <CENTER> 
        <TABLE COLS="2">
    		<TR><td COLSPAN="2" ALIGN="right"><LABEL ID=7282>Variables disponibles</LABEL></td></TR>
    		<TR><td COLSPAN="2"><hr></td></TR>
            <TR><td COLSPAN="2"></td></TR>
    		<TR><td ALIGN="middle" COLSPAN=2>
                <DIV ID="Scroll" style="OVERFLOW: auto; WIDTH: 400px; HEIGHT: 250px; BACKGROUND-COLOR: ivory">
                <%	insDefineHeader()
	insGetVariables()%>
                </DIV>
    		    </td>
    		</TR>
    		<TR>
    		    <TD><BR></TD>
    		</TR>
    	        <TR>
    	            <TD COLSPAN="2" ALIGN=right><%	If Request.QueryString.Item("sCod") = "LT001" Then
		Response.Write(mobjValues.ButtonAcceptCancel("insAccept(2)", "insCancel()", False))
	Else
		Response.Write(mobjValues.ButtonAcceptCancel("insAccept()", "insCancel()", False))
	End If%></TD>
    	        </TR>		
    	</TABLE>
        </CENTER>     
<%Else
	
	If Request.QueryString.Item("Location") = String.Empty Then
		Response.Write("<SCRIPT>")
		Response.Write("insWriteDoc();")
		Response.Write("insLoadWord();")
		Response.Write("window.close();")
		Response.Write("</" & "SCRIPT>")
	End If
End If%>
</FORM>    
</BODY>
</HTML>
<%mobjGrid = Nothing
mobjValues = Nothing%>

<%'^Begin Footer Block VisualTimer Utility 1.1 09/05/2003 10:49:58 a.m.
Call mobjNetFrameWork.FinishPage("Variables")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>










