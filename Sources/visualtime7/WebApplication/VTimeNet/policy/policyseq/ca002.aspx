<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Se define la variable para la carga de datos del Grid de la ventana		
    Dim mclsAgreement_pol As ePolicy.Agreement_pol
    Dim mcolAgreement_pols As Object
    Dim mcolPolicy As ePolicy.Agreement_pols

    Dim lintIndex As Byte
    Dim mstrAction As String

'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	    
	mobjGrid.ActionQuery = Session("bQuery")
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
            .AddClientColumn(0, GetLocalResourceObject("tctCodeColumnCaption"), "tctClient", vbNullString, , GetLocalResourceObject("tctCodeColumnToolTip"), "InsChangeClient(this)", False, "lblCliename", False, , , , , False)
            .AddPossiblesColumn(0, GetLocalResourceObject("cbeCod_AgreeColumnCaption"), "cbeCod_Agree", "tabAgreement_pol", eFunctions.Values.eValuesType.clngWindowType, , True, , , , "InsChangeAgree(this)", , 5, GetLocalResourceObject("cbeCod_AgreeColumnToolTip"))
            .AddTextColumn(0, GetLocalResourceObject("tctNameAgreeColumnCaption"), "tctAgreeName", 50, vbNullString, , GetLocalResourceObject("tctPrintNameColumnToolTip"), , , , True)
    
            
        End With
	
        With mobjGrid
            '+ Se definen las propiedades generales del grid
            With .Columns("cbeCod_Agree").Parameters
                .Add("sClient", Request.QueryString.Item("sClient"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End With
            .Codispl = "CA002"
            .Codisp = "CA002"
            .Top = 150
            .Left = 100
            .Width = 500
            .Height = 300
            .WidthDelete = 500
		
            .bCheckVisible = Request.QueryString.Item("Action") <> "Add"
            .Columns("Sel").GridVisible = Not Session("bQuery")
		

		
            .sEditRecordParam = "nCod_Agree=" & Request.QueryString.Item("nCod_Agree") & "&sClient=" & Request.QueryString.Item("sClient")
		
            .sDelRecordParam = "nCod_Agree='+ marrArray[lintIndex].cbeCod_Agree + '" & "&sClient='+ marrArray[lintIndex].tctClient + '"
		
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
End Sub

'% insPreDP080: Obtiene los cargos de los aportes
'-----------------------------------------------------------------------------
Private Sub insPreCA002()
	'-----------------------------------------------------------------------------                                   
        If mcolPolicy.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("npolicy"), Session("nCertif"), Session("dEffecdate"), Session("nTransaction"), Session("nUsercode")) Then
		
            If mcolPolicy.Count > 0 Then
                
                mobjGrid.DeleteButton = True
                For Each Me.mclsAgreement_pol In mcolPolicy
                    With mobjGrid
                        
                        .Columns("tctClient").DefValue = mclsAgreement_pol.sClient
                        .Columns("cbeCod_Agree").Parameters.Add("sClient", mclsAgreement_pol.sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Columns("cbeCod_Agree").DefValue = mclsAgreement_pol.nCod_Agree
                        .Columns("tctAgreeName").DefValue = mclsAgreement_pol.sCliename
                        
                    End With
                    Response.Write(mobjGrid.DoRow())
                Next mclsAgreement_pol
            End If
        End If
	Response.Write(mobjGrid.closeTable())
End Sub

    '% insPreCA002Upd: Realiza la eliminación de cargos
'-----------------------------------------------------------------------------
Private Sub insPreCA002Upd()
	'-----------------------------------------------------------------------------
	'- Objeto para manejo de los cargos de contribuciones
        Dim mclsAgreement_pol As ePolicy.Agreement_pol
	
	Dim lblnPost As Object
	
	If Request.QueryString.Item("Action") = "Del" Then
            mclsAgreement_pol = New ePolicy.Agreement_pol
		'+ Muestra el mensaje para eliminar registros
		Response.Write(mobjValues.ConfirmDelete())
		
            Call mclsAgreement_pol.insPostCA002(Request.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("npolicy"), Session("nCertif"), Request.QueryString.Item("sClient"), Session("dEffecdate"), mobjValues.StringToType(Request.QueryString.Item("ncod_agree"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
	End If
	mclsAgreement_pol = Nothing
        Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valPolicyseq.aspx", "CA002", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
    End Sub
</script>
<SCRIPT LANGUAGE=javascript>
// % InsChangeClient: Despliega los datos del cliente
//-------------------------------------------------------------------------------------------
function InsChangeClient() {
    //-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
    cbeCod_Agree.Parameters.Param1.sValue = tctClient.value;
    cbeCod_Agree.value = "";
    }
}

//-------------------------------------------------------------------------------------------
function InsChangeAgree() {
//-------------------------------------------------------------------------------------------
    var lstrAction = '<%=mstrAction%>'
    var lblnShowDefValues 
        //+ Si la acción es agregar siempre se habilita la variable para llamar al showDefValues.
        //if (lstrAction == 'Add')
            lblnShowDefValues = true;
       // else
         //   lblnShowDefValues = false;

        //+ Esto con la idea de que la primera vez que entre a la popup no lo haga
        if (lblnShowDefValues == true) {
            with (self.document.forms[0]) {
                 insDefValues('Agreement', "nCod_Agree=" + cbeCod_Agree.value  + "&sCodispl=" + "CA002" , '/VTimeNet/Policy/PolicySeq');
                }
            }
        } 

</script>
<%Response.Expires = -1

'- Se crean las instancias de las variables modulares
    mobjValues = New eFunctions.Values
    mobjMenu = New eFunctions.Menues
    mclsAgreement_pol = New ePolicy.Agreement_pol
    mcolPolicy = New ePolicy.Agreement_pols
    mobjGrid = New eFunctions.Grid

    mobjGrid.sCodisplPage = "CA002"
    mobjValues.sCodisplPage = "CA002"
    mstrAction = Request.QueryString.Item("Action")
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE=javascript>
//+ Esta línea guarda la version procedente de VSS
    document.VssVersion="$$Revision: 3 $|$$Date: 13/02/06 11:28 $"
</SCRIPT>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("CA002"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "CA002", "CA002.aspx"))
		.Write("<SCRIPT> var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
	End If
End With
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCA002" ACTION="valPolicyseq.aspx?sZone=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%Response.Write(mobjValues.ShowWindowsName("CA002"))
    lintIndex = 0
%>        
    <BR>
<%
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCA002()
Else
	Call insPreCA002Upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
mclsAgreement_pol = Nothing
mcolAgreement_pols = Nothing
%> 
</FORM>
</BODY>
</HTML>