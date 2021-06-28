<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLetter" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:49:57 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de	las	funciones generales	de carga de	valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim mrecQuery As eRemoteDB.Query
    
'% insDefineHeader
'----------------------------------------
    Private sub insDefineHeader
'----------------------------------------        
        mobjgrid = New efunctions.grid
		'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:49:57 a.m.
		mobjgrid.sSessionID = Session.SessionID
		'~End Body Block VisualTimer Utility
	
		Call  mobjGrid.SetWindowParameters( Request.QueryString("sCodispl"),  Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
    
		mobjgrid.sCodisplPage = "LT001_K"

        mobjGrid.Codispl = "LT001"
        mobjGrid.Codisp = "LT001_K"  
        mobjGrid.Width = 600
        mobjGrid.Height  = 355
        mobjGrid.ActionQuery = (Request.QueryString("nMainAction") = "401" Or Request.QueryString("nMainAction")=String.Empty)
        mobjGrid.nMainAction = 0 & Request.QueryString("nMainAction")
        
        with mobjGrid.Columns 
            .AddNumericColumn(7250,"Modelo de Carta","tcnLetterNum",4,"",,"Numero que identifica el modelo o formato de carta.",,,,,,Request.QueryString("Action")<>"Add")
            mobjGrid.Columns("tcnLetterNum").OnChange = "insChangeOption(this);" 

            .AddPossiblesColumn(7251,"Idioma","cbeLanguage", "Table85",1,,,,,,,Request.QueryString("Action")<>"Add",,"Idioma en el que se encuentra el contenido de la carta.")
            .AddTextColumn(7252,"Descripción", "tctDescript", 30,"",,"Nombre o descripción del modelo de carta en referencia al contenido de la misma.")
            
            .AddHiddenColumn("tcdEffecDate",Nothing)

            If Request.QueryString("Type")= "PopUp" then
                .AddCheckColumn(7253,"Requiere Seguimiento","chkCtroLettInd","",,,,,"Indicador de seguimiento de las cartas generadas utilizando el modelo de carta en tratamiento.")
                .AddCheckColumn(7254,"Envío de Direcciones inválidas", "chksDelivInvalidind","",,,,,"Indica que (en caso de estar marcado) la correspondencia asociada al modelo de carta en tratamiento, debe ser enviada inclusive a direcciones marcadas como ")
                '.AddAnimatedColumn(7256,"Ver Documento", "btnLocation1","/VTimeNet/images/btn_ValuesOff.png","Permite consultar el modelo de carta.")
                .AddNumericColumn(7255,"Tiempo mínimo de respuesta (días)","tcnMinTimeAns",3,"",,"Es el tiempo mínimo, en días, que debe esperar el sistema para que se registren las fechas de impresión, entrega y respuesta de cada una de las cartas generadas en una solicitud.",,,,,,true)
                .AddAnimatedColumn(7256,"Ver Documento", "btnLocation","/VTimeNet/images/A302Off.png","Permite consultar el modelo de carta.",, "insOpenDocument();")
                .AddFileColumn(7257,"Nombre del Archivo", "tctFileName", 40,,,,vbNullString)
            Else
				.AddCheckColumn(7253,"Requiere Seguimiento", "chkCtroLettInd","",,,,true,"Indicador de seguimiento de las cartas generadas utilizando el modelo de carta en tratamiento.")
                .AddCheckColumn(7254,"Envío de Direcciones inválidas", "chksDelivInvalidind","",,,,True,"Indica que (en caso de estar marcado) la correspondencia asociada al modelo de carta en tratamiento, debe ser enviada inclusive a direcciones marcadas como ")
                .AddHiddenColumn("tcnMinTimeAns",Nothing)
                '.AddNumericColumn(7255,"Tiempo mínimo de respuesta (días)","tcnMinTimeAns",3,"",,"Es el tiempo mínimo, en días, que debe esperar el sistema para que se registren las fechas de impresión, entrega y respuesta de cada una de las cartas generadas en una solicitud.",,,,,,true)
                .AddAnimatedColumn(7256,"Ver Documento", "btnLocation","/VTimeNet/images/btn_ValuesOff.png","Permite consultar el modelo de carta.")
            End If
            mobjGrid.Columns("chkCtroLettInd").OnClick = "insHandDays(this);" 
            mobjGrid.Columns("tctDescript").EditRecord = not mobjGrid.ActionQuery
             '.AddHiddenColumn("tcnValue",Nothing)
        end with
        mobjGrid.Columns("Sel").GridVisible = Request.QueryString("nMainAction") = 302
        If Request.QueryString("Reload") = "1" then
            mobjGrid.sReloadIndex = Request.QueryString("ReloadIndex")
        End If        
        
        mobjGrid.sReloadAction = Request.QueryString("ReloadAction")
        mobjGrid.DeleteButton = Request.QueryString("nMainAction") = 302
        mobjGrid.AddButton = Request.QueryString("nMainAction") =  301 or   Request.QueryString("nMainAction") =  302
    End Sub

'% inspreLT001
'----------------------------------------
Private sub inspreLT001
'----------------------------------------    
	Dim lcolLetters
	Dim lobjLetter 
	Dim lintIndex
	
    lcolLetters = new eLetter.Letters
    lobjLetter  = new eLetter.Letter
    mrecQuery = New eRemoteDB.Query

        
	mobjGrid.sDelRecordParam = "nLetterNum=' + marrArray[lintIndex].tcnLetterNum + '&nLanguage=' + marrArray[lintIndex].cbeLanguage +'&dEffecdate=' + marrArray[lintIndex].tcdEffecDate + '"
    With lcolLetters
        lintIndex = 0

		If .Find(date.Today) then
            For Each lobjLetter In lcolLetters
                mobjGrid.Columns("tcnLetterNum").DefValue = lobjLetter.nLetterNum
                mobjGrid.Columns("Sel").Disabled = (lobjLetter.nValue = 1)  
				mobjGrid.Columns("tctDescript").DefValue = lobjLetter.sDescript
                mobjGrid.Columns("cbeLanguage").DefValue = lobjLetter.nLanguage
				mobjGrid.Columns("tcdEffecDate").DefValue = lobjLetter.deffecdate
                
                If lobjLetter.sCtroLettInd = "1" Then
                    mobjGrid.Columns("chkCtroLettInd").Checked =  "1"
                Else 
                    mobjGrid.Columns("chkCtroLettInd").Checked =  "0"
                End If
				
				
                If lobjLetter.sDelivInvalidInd = "1" Then
                    mobjGrid.Columns("chksDelivInvalidInd").Checked =  "1"
                Else 
                    mobjGrid.Columns("chksDelivInvalidInd").Checked =  "0"
                End If
                
				mobjGrid.Columns("tcnMinTimeAns").DefValue  = lobjLetter.nMinTimeAns
				
                mobjGrid.Columns("btnLocation").HRefScript  = "insOpenDocument(" & lintindex & ");"
                mobjGrid.Columns("btnLocation").Disabled  = False
                
                
                Response.write(mobjGrid.DoRow)
                lintIndex = lintIndex + 1
            Next 
        End If
	End with
    Response.Write(mobjGrid.closeTable)
    lcolLetters = Nothing
End Sub

'% inspreLT001
'----------------------------------------
    Private sub inspreLT001Upd
'----------------------------------------    
        if Request.QueryString("Action")= "Del" then
            call insDelItem()
            Response.write(mobjValues.ConfirmDelete())
        end if

        response.write(mobjGrid.DoFormUpd(Request.QueryString("Action"),"ValLetter.aspx",Request.QueryString.Item("sCodispl"),Request.QueryString("nMainAction"),mobjValues.ActionQuery,Request.QueryString("Index")))

        If Request.QueryString("Action") <> "Del" then

           response.Write("<SCRIPT>insHandDays(self.document.forms[0].chkCtroLettInd);</" & "SCRIPT>")
            
        End If
        
        If Request.QueryString("Action") = "Add" then
            Dim lclsNumerator As eLetter.LettRequest
	        Dim nNumerator As Integer 
	    	
	    	lclsNumerator = New eLetter.LettRequest
	    	
	    	nNumerator = lclsNumerator.FindNumerator(71, 0)
	
	        lclsNumerator = Nothing
            response.Write("<SCRIPT>insNumerator('" & nNumerator & "');</" & "SCRIPT>")
       End If   
       
    End Sub
    
'% insDelItem    
'------------------------------------
Private sub insDelItem    
'------------------------------------
    Dim lobjLetter As eLetter.Letter
    lobjLetter = new eLetter.Letter
    
    'lobjLetter = Server.Createobject("eLetter.Letter")
        Call lobjLetter.insPostLT001("Delete", Request.QueryString("nLetterNum"), strnull, mobjValues.StringToType(Request.QueryString("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString("nLanguage"), eFunctions.Values.eTypeData.etdInteger), strnull, Session("nUserCode"), strnull, intnull, strnull)

    lobjLetter =  Nothing

End sub
</script>

<%  
    Response.Expires = -1441
    '^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:49:57 a.m.
   
    mobjNetFrameWork = new eNetFrameWork.Layout
    mobjValues = new eFunctions.Values


    mobjNetFrameWork.sSessionID = Session.SessionID
    Call mobjNetFrameWork.BeginPage("LT001_K")
    '~End Header Block VisualTimer Utility
	'- Objeto para el manejo de las funciones generales de carga de valores
    '^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:49:57 a.m.
    mobjValues.sSessionID = Session.SessionID
    '~End Body Block VisualTimer Utility
    mobjValues.sCodisplPage = "LT001_K"
%>
<HTML>
<HEAD>
	<%=mobjValues.StyleSheet()%>
	<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
    <%  if Request.QueryString("Type") <> "PopUp" then	%>
	<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
    <%  end if %>
	<!--#include virtual ="~/VTimeNet/Includes/Constantes.aspx"-->
	<!--#include virtual ="~/VTimeNet/Includes/Collection.aspx"-->


<%Response.Write("<SCRIPT>")
  Response.Write("var nMainAction = 0;")
  Response.Write("var bIsLoading = true;")

  If Request.QueryString("nMainAction") <> String.Empty Then 
	Response.Write( "nMainAction = "  & Request.QueryString("nMainAction") & ";")
  End If
  Response.Write("</SCRIPT>")
 %>
<SCRIPT>

//**+Objetive: This line keep the source safe version
//+Objeto: Esta línea guarda la versión procedente de VSS 
//------------------------------------------------------------------------------------------
    document.VssVersion="$$Revision: 4 $|$$Date: 6/30/07 11:41 p $$Author: oa $"
//-----------------------------------------------------------------------------------------

function insHandDays(field){
//--------------------------------------------------------------------------------    
    
    self.document.forms[0].tcnMinTimeAns.disabled=(!field.checked); 
    if (!field.checked)
        self.document.forms[0].tcnMinTimeAns.value='';
        
}

//-----------------------------------------------------------------------------------------
function insNumerator(field){
//--------------------------------------------------------------------------------    
    self.document.forms[0].tcnLetterNum.value = field;
    self.document.forms[0].tcnLetterNum.disabled = true;
}

//--------------------------------------------------------------------------------    
    

//--------------------------------------------------------------------------------    
function insOpenDocument(lintIndex){
//--------------------------------------------------------------------------------    
    var lstrQueryString;
    var lstrAction;
    var lintCall;
    
    lstrAction = "<%=Request.QueryString("Action")%>";
 <%  If Request.QueryString("Type") = "PopUp" Then %>
		if (self.document.forms[0].tcnLetterNum.value > 0 && self.document.forms[0].cbeLanguage.value > 0)
		{
			lstrQueryString = "Variables.aspx?Type=upd&Action=" + lstrAction + "&Location=" + document.forms[0].tctFileName.value + "&nLetterNum=" + document.forms[0].tcnLetterNum.value  + "&nLanguage=" + document.forms[0].cbeLanguage.value + "&sCod=LT001";
			lintCall = 1;
		}
		else
		{
			lintCall = 0;
		}
<%  Else%>
		lstrQueryString = "Variables.aspx?Type=Qry&Action=" + lstrAction + "&Location=" + "&nLetterNum=" + marrArray[lintIndex].tcnLetterNum + "&nLanguage=" + marrArray[lintIndex].cbeLanguage;
		lintCall = 1;
<%  End If%>        
    
    //alert(lintCall)
    if (lintCall==1)
		ShowPopUp(lstrQueryString,"Values", 425,400,"no","no", 100, 100, "no");
	    	
 
}
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
    if (nMainAction !=0 && bIsLoading){return;}

  switch(llngAction){
	    case 301:
	    case 302:
	    case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction;
	        break;
	    default:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + 401;
	}
}

//--------------------------------------------------------------------------------    
function insStateZone(){("OptCollect(0)");
//--------------------------------------------------------------------------------    

}

//--------------------------------------------------------------------------------    
function insFinish(){
//--------------------------------------------------------------------------------    
   //alert(document.forms[0].tctFileName.value);
    return(true)
}
//% insCancel: ejecuta la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){insPreZone(401)}   

//--------------------------------------------------------------------------------------------
    

//**% insChangeOption: Entrusts filling the fields of the popup if the model of letter exists.
//% insChangeOption: Se encarga de llenar los campos de la popup si el modelo de carta existe.
//------------------------------------------------------------------------------------------ 
function insChangeOption(field)
{
<%	If Request.QueryString("Type") = "PopUp" And Request.QueryString("Action") = "Add" Then %>
		with(self.document.forms[0])
		{
			if (tcnLetterNum.value != "")
			{
				lstrQueryString = "DefValuesLett.aspx?Field=LT001&nLetterNum=" + tcnLetterNum.value;
				ShowPopUp(lstrQueryString,"Values", 50,50,"no","no", 2000, 2000, "no");
			}
			else
			{
				tctDescript.value='';
				tctDescript.disabled = false; 
				chkCtroLettInd.checked=false;
				chkCtroLettInd.disabled = false;
				tcnMinTimeAns.disabled = true;
				tcnMinTimeAns.value='';
				chksDelivInvalidind.checked=false;
				chksDelivInvalidind.disabled=false;
			}
		}
<% End If%>  
}

</SCRIPT>   
    <%  if Request.QueryString("Type") <> "PopUp" then
            mobjMenu= new eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:49:57 a.m.
    mobjMenu.sSessionID = Session.SessionID
    '~End Body Block VisualTimer Utility
    Response.Write(mobjMenu.MakeMenu("LT001","LT001_K.aspx",1, Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"), Session("sSche_code")))
    'Response.Write(mobjMenu.MakeMenu("LT002", "LT002_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
        mobjMenu = Nothing 
        end if%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" ID="FORM" NAME="frmCO001_k" ACTION="valLetter.aspx?time=1&FName=<%=Request.Form.Item("tctFileName")%>" ENCTYPE="multipart/form-data">
<%If Request.QueryString("Type") <> "PopUp" then %>
        <BR><BR><BR>
<%End if%>
    <%	Response.Write(mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript")))%>

<%  insDefineHeader
    If Request.QueryString("Type") <>  "PopUp" Then
        inspreLT001
    Else
        inspreLT001Upd
    End If
    
    mobjGrid = Nothing
    mobjValues = Nothing
%>
<script>bIsLoading=false;</script>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 09/05/2003 10:49:57 a.m.
	Call mobjNetFrameWork.FinishPage("LT001_K")
	mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>



