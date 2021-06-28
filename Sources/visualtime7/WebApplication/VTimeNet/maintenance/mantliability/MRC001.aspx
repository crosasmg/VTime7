<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Se define la variable mobjGrid para el manejo del Grid de la ventana
    Dim mobjGrid As eFunctions.Grid

    '- Objeto para el manejo de las zonas de la página
    Dim mobjMenu As eFunctions.Menues


    '%insDefineHeader: Se definen las columnas del grid
    '------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '------------------------------------------------------------------------------
        mobjGrid = New eFunctions.Grid
	
        '+Se definen todas las columnas del Grid
        With mobjGrid.Columns
		
        
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeArticleColumnCaption"), "cbeArticle", "Table118", eFunctions.Values.eValuesType.clngComboType, , , , , , "InsParamValue(this);", , 2, GetLocalResourceObject("cbeArticleColumnTooltip"), eFunctions.Values.eTypeCode.eNumeric)
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeDetailArtColumnCaption"), "cbeDetailArt", "tabtab_in_bus", eFunctions.Values.eValuesType.clngWindowType, "", True, , , , , , 2, GetLocalResourceObject("cbeDetailArtColumnTooltip"))
		
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 5, "", False, GetLocalResourceObject("tcnRateColumnTooltip"), False, 2, , , , False)
		
        End With
	
        With mobjGrid
            .Columns("cbeDetailArt").Parameters.Add("nArticle",  Request.QueryString.Item("nArticle"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Codispl = "MRC001"
            .Codisp = "MRC001"
            .Top = 100
            .Height = 224
            .Width = 580
            .ActionQuery = mobjValues.ActionQuery
            .bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
            .Columns("Sel").GridVisible = Not .ActionQuery
            .Columns("cbeArticle").EditRecord = True
            .Columns("cbeArticle").Disabled = Request.QueryString.Item("Action") = "Update"
		
            '.Columns("cbeDetailArt").Disabled = True
		
            .sDelRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nCover=" & Request.QueryString.Item("nCover") & "&dEffecDate=" & Request.QueryString.Item("dEffecDate") & "&nArticle='+ marrArray[lintIndex].cbeArticle + '" & "&nDetailArt='+ marrArray[lintIndex].cbeDetailArt + '"
            '.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nCover=" & Request.QueryString.Item("nCover") & "&dEffecDate=" & Request.QueryString.Item("dEffecDate") & "&nArticle='+ marrArray[" & lintIndex & "].cbeArticle + '"
            .sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nCover=" & Request.QueryString.Item("nCover") & "&dEffecDate=" & Request.QueryString.Item("dEffecDate") & "&nArticle='+ marrArray[lintIndex].cbeArticle + '"
		
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
    End Sub

'%insPreMRC001. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
    Private Sub insPreMRC001()
        '------------------------------------------------------------------------------
        Dim lcolTar_rc_bass As eBranches.Tar_rc_bass
        Dim lclsTar_rc_bas As Object
        Dim lintIndex As Short
	
        With Request
            lcolTar_rc_bass = New eBranches.Tar_rc_bass
            With mobjGrid
                If lcolTar_rc_bass.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate)) Then
                    lintIndex = 0
                    For Each lclsTar_rc_bas In lcolTar_rc_bass
                        .Columns("cbeArticle").DefValue = lclsTar_rc_bas.nCommergrp
                        .Columns("cbeDetailArt").Parameters.Add("nArticle", lclsTar_rc_bas.nCommergrp)
                        .Columns("cbeDetailArt").DefValue = lclsTar_rc_bas.nDetailArt
                        .Columns("tcnRate").DefValue = lclsTar_rc_bas.nRate
                        .sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nCover=" & Request.QueryString.Item("nCover") & "&dEffecDate=" & Request.QueryString.Item("dEffecDate") & "&nArticle='+ marrArray[" & lintIndex & "].cbeArticle + '" & "&nDetailArt='+ marrArray[" & lintIndex & "].cbeDetailArt + '"
					
                        lintIndex = lintIndex + 1
                        Response.Write(mobjGrid.DoRow())
                    Next lclsTar_rc_bas
                End If
            End With
		
        End With
        Response.Write(mobjGrid.closeTable())
	
        lclsTar_rc_bas = Nothing
        lcolTar_rc_bass = Nothing
    End Sub

'% insPreMRC001Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
    Private Sub insPreMRC001Upd()
        '------------------------------------------------------------------------------
        Dim lclsTar_rc_bas As eBranches.Tar_rc_bas
        With Request
            If .QueryString.Item("Action") = "Del" Then
                Response.Write(mobjValues.ConfirmDelete())
                lclsTar_rc_bas = New eBranches.Tar_rc_bas
                Call lclsTar_rc_bas.InsPostMRC001(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), Session("nUsercode"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nArticle"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nDetailArt"), eFunctions.Values.eTypeData.etdInteger), 0, mobjValues.StringToType(.QueryString.Item("nArticle"), eFunctions.Values.eTypeData.etdInteger))
            End If
		
            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valmantliability.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
        End With
        lclsTar_rc_bas = Nothing
    End Sub

</script>
<%  Response.Expires = -1441

    mobjValues = New eFunctions.Values
    mobjValues.sSessionID = Session.SessionID
    mobjValues.sCodisplPage = "MRC001"
%>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->


<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE=JavaScript>
     

//% InsParamValue: Asigna Articulo
//---------------------------------------------------------------------------------------------------
function InsParamValue(Field){
//---------------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		switch(Field.name){
            case "cbeArticle":
                if (cbeArticle.value > 0){
					cbeDetailArt.Parameters.Param1.sValue = cbeArticle.value
					cbeDetailArt.disabled = false;
					btncbeDetailArt.disabled = false;
					cbeDetailArt.value = "";
					UpdateDiv('cbeDetailArtDesc',"");
					
		        }
		        else
		        {
					cbeDetailArt.disabled = true
					btncbeDetailArt.disabled = true
					cbeDetailArt.value = "";
					UpdateDiv('cbeDetailArtDesc',"");
		        }     
            break;
        }
			
	}
} 

</SCRIPT>
<HTML>
  <HEAD>
	<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
    mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery))
    With Response
	    .Write(mobjValues.StyleSheet())
	    .Write("<SCRIPT>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
	    If Request.QueryString.Item("Type") <> "PopUp" Then
		    mobjMenu = New eFunctions.Menues
		    .Write(mobjMenu.setZone(2, "MRC001", "MRC001.aspx"))
            mobjMenu = Nothing
	    End If
    End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMRC001" ACTION="valmantliability.aspx?sZone=2">
<%
    Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
    Call insDefineHeader()
    If Request.QueryString.Item("Type") <> "PopUp" Then
	    Call insPreMRC001()
    Else
	    Call insPreMRC001Upd()
    End If
    mobjValues = Nothing
    mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>
