<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues

    '- Objeto para el manejo del grid    
    Dim mobjGrid As eFunctions.Grid

    '- Se declara la variable que guarda el módulo seleccionado
    Dim mintModulec As Integer

    '- Se declara la variable que guarda la cobertura seleccionado
    Dim mintCover As Short

    Dim mcolGen_covers As eProduct.Gen_covers
    Dim mblnDataFound As Boolean


    '% insDefineHeader: se definen los campos del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------
        Dim lclsGen_cover As eProduct.Gen_cover

        mobjGrid = New eFunctions.Grid
        lclsGen_cover = New eProduct.Gen_cover

        mobjGrid.sCodisplPage = "DP033"

        '+ Se definen las columnas del grid    
        With mobjGrid.Columns
            If Request.QueryString.Item("Type") <> "PopUp" Then
                Call .AddAnimatedColumn(0, GetLocalResourceObject("sLinkColumnCaption"), "sLink", "/VTimeNet/Images/clfolder.png", GetLocalResourceObject("sLinkColumnToolTip"))
            End If
            '+ Se corrige el largo del campo ncover ya que debe ser largo 5 según modelo de datos.
            Call .AddNumericColumn(41339, GetLocalResourceObject("nCoverColumnCaption"), "nCover", 5, vbNullString,  , GetLocalResourceObject("nCoverColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
            Call .AddPossiblesColumn(41337, GetLocalResourceObject("nCovergenColumnCaption"), "nCovergen", lclsGen_cover.getTableDP033(Session("sBrancht")), eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  , 5, GetLocalResourceObject("nCovergenColumnToolTip"))
            Call .AddPossiblesColumn(41338, GetLocalResourceObject("sStatregtColumnCaption"), "sStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, "2",  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("sStatregtColumnToolTip"))
            Call .AddHiddenColumn("tcnModulec", CStr(0))
            Call .AddHiddenColumn("sOldStatregt", "2")
        End With

        '+ Se definen las propiedades generales del grid
        With mobjGrid
            .Height = 200
            .Width = 490
            .Codispl = "DP033"
            .bCheckVisible = Request.QueryString.Item("Action") <> "Add"
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
            '+ Se establece es estado inicial del campo "Estado" según la acción y el estado			
            .Columns("sStatregt").Disabled = Request.QueryString.Item("Action") = "Add" Or Request.QueryString.Item("sStatregt") = "2"

            .sDelRecordParam = "nModulec='+ marrArray[lintIndex].tcnModulec + '&nCover='+ marrArray[lintIndex].nCover + '&nCovergen='+ marrArray[lintIndex].nCovergen +'"

            If mcolGen_covers.bModule Then
                .sEditRecordParam = "nModulec=' + self.document.forms[0].cbeModulec.value + '"
            End If
            If Request.QueryString.Item("Reload") = "1" And Request.QueryString.Item("ReloadAction") <> "Add" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If

            '+ Se excluye el estado "En proceso de instalación"
            If Request.QueryString.Item("Action") = "Update" And Request.QueryString.Item("sStatregt") <> "2" Then
                .Columns("sStatregt").TypeList = 2
                .Columns("sStatregt").List = CStr(2)
                .Columns("sStatregt").BlankPosition = False
            End If
            .Columns("Sel").GridVisible = Not Session("bQuery")
            .Columns("nCovergen").EditRecord = True
            .Columns("nCovergen").Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("nCovergen").Parameters.Add("nCoverGen", mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        End With
        lclsGen_cover = Nothing
    End Sub

    '% insPreDP033: se cargan los controles de la página
    '--------------------------------------------------------------------------------------------
    Private Sub insPreDP033()
        '--------------------------------------------------------------------------------------------
        Dim lintIndex As Short
        Dim lclsGen_cover As Object

        lintIndex = 0

        '+ En caso que tenga módulos asociados, se crea el campo "Módulos"
        If mcolGen_covers.bModule Then

            Response.Write("		<TABLE WIDTH=""100%"">" & vbCrLf)
            Response.Write("		    <TR>" & vbCrLf)
            Response.Write("			    <TD WIDTH=10%><LABEL ID=14431>" & GetLocalResourceObject("cbeModulecCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("	 	        <TD>")

            With mobjValues
                .Parameters.Add("nBranch", .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Response.Write(mobjValues.PossiblesValues("cbeModulec", "tabTab_modul", eFunctions.Values.eValuesType.clngComboType, CStr(mintModulec), True,  ,  ,  ,  , "insChangeModule(this.value);",  , 4, ""))
            End With

            Response.Write("" & vbCrLf)
            Response.Write("				</TD>" & vbCrLf)
            Response.Write("		    </TR>" & vbCrLf)
            Response.Write("		</TABLE>")


        End If

        mobjValues.ActionQuery = Session("bQuery")

        If mblnDataFound Then
            Response.Write(mobjValues.HiddenControl("tcnCount", mcolGen_covers.mobjCover.Count))
            For Each lclsGen_cover In mcolGen_covers.mobjCover
                With mobjGrid
                    .Columns("sLink").HRefScript = "ShowSubSequence(" & lintIndex & ")"
                    .Columns("nCovergen").DefValue = lclsGen_cover.nCovergen
                    .Columns("tcnModulec").DefValue = lclsGen_cover.nModulec
                    .Columns("nCover").DefValue = lclsGen_cover.nCover
                    .Columns("sStatregt").DefValue = lclsGen_cover.sStatregt
                    .Columns("sOldStatregt").DefValue = lclsGen_cover.sStatregt
                    .Columns("Sel").OnClick = "insDefValues(""CoverData"",""Index=" & lintIndex & "&nCover=" & lclsGen_cover.nCover & """)"
                    If mcolGen_covers.bModule Then
                        .sEditRecordParam = .sEditRecordParam & "&"
                    End If
                    .sEditRecordParam = .sEditRecordParam & "sStatregt=' + marrArray[" & lintIndex & "].sStatregt + '"
                    Response.Write(.DoRow)
                    lintIndex = lintIndex + 1
                End With
            Next lclsGen_cover
        End If
        Response.Write(mobjGrid.closeTable)
        Response.Write(mobjValues.BeginPageButton)

        If Request.QueryString.Item("ReloadAction") = "Add" Then
            '+ Se invoca la subsecuencia de Coberturas			
            Response.Write("<SCRIPT>ShowSubSequence(-1," & Request.QueryString.Item("nCover") & "," & Request.QueryString.Item("nCovergen") & "," & mintModulec & ")</" & "Script>")
        End If

        If mcolGen_covers.bModule And Not Session("bQuery") Then
            Response.Write("<SCRIPT>InsDisabledAdd('" & mintModulec & "')</" & "Script>")
        End If

        mcolGen_covers = Nothing
        lclsGen_cover = Nothing
    End Sub

    '% insPreDP033Upd: Se realiza el manejo de los campos del grid 
    '--------------------------------------------------------------------------------------------
    Private Sub insPreDP033Upd()
        '--------------------------------------------------------------------------------------------
        Dim lclsGen_cover As eProduct.Gen_cover
        Dim lclsTab_reqexc As Object
        Dim lclsErrors As Object

        With Request
            If .QueryString.Item("Action") = "Del" Then
                lclsGen_cover = New eProduct.Gen_cover
                Response.Write(mobjValues.ConfirmDelete())

                If lclsGen_cover.insPostDP033Upd(.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mintModulec, mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nCovergen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), vbNullString, Session("sBrancht")) Then
                    Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
                End If
            End If

            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValProductSeq.aspx", "DP033", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))

            If .QueryString.Item("Action") <> "Del" Then
                With Response
                    If Request.QueryString.Item("Action") <> "Add" Then
                        .Write("<SCRIPT>" & "with(self.document.forms[0]){" & "nCovergen.disabled=true;" & "btnnCovergen.disabled=true;" & "}")
                    Else
                        .Write("<SCRIPT>" & "with(self.document.forms[0]){" & "nCovergen.disabled=true;" & "}")
                    End If
                    If Request.QueryString.Item("Action") = "Add" Then
                        .Write("insDefaultValues();")
                    End If

                    .Write("self.document.forms[0].elements[""tcnModulec""].value = 0;")
                    If Request.QueryString.Item("nModulec") <> vbNullString Then
                        .Write("self.document.forms[0].elements[""tcnModulec""].value = " & Request.QueryString.Item("nModulec") & ";")
                    End If
                    .Write("</" & "Script>")
                End With
            End If
        End With

        lclsGen_cover = Nothing
        lclsTab_reqexc = Nothing
        lclsErrors = Nothing
    End Sub

</script>
<%Response.Expires = -1

mobjMenu = New eFunctions.Menues
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "DP033"

mintModulec = 0
mintCover = 0

If Request.QueryString.Item("nModulec") <> vbNullString Then
	mintModulec = CShort(Request.QueryString.Item("nModulec"))
End If

If Request.QueryString.Item("nCover") <> vbNullString Then
	mintCover = CShort(Request.QueryString.Item("nCover"))
End If

mcolGen_covers = New eProduct.Gen_covers

If Request.QueryString.Item("Type") <> "PopUp" Then
	mblnDataFound = mcolGen_covers.insPreDP033(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sBrancht"), mintModulec)
End If
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:02 $|$$Author: Nvaplat61 $"    

//+ Se recarga la página para que muestre las coberturas del módulo seleccionado
//----------------------------------------------------------------------------------------------------------------------
function insChangeModule(nModulec){
//----------------------------------------------------------------------------------------------------------------------
	var lstrstring = '';
	if (nModulec != '<%=Request.QueryString.Item("nModulec")%>'){
		lstrstring += document.location;
		lstrstring = lstrstring.replace(/&nModulec=.*/, "");
		lstrstring = lstrstring + "&nModulec="+nModulec;
		document.location.href = lstrstring;
	}
}

//+ Se encarga de habilitar o deshabilitar el botón de agregar.
//----------------------------------------------------------------------------------------------------------------------
function InsDisabledAdd(nModulec){
//----------------------------------------------------------------------------------------------------------------------
	with(self.document){
		cmdAdd.disabled = nModulec==0;
		if(typeof(cmdDelete)!='undefined')
			cmdDelete.disabled = nModulec==0;		
	}
}

// insDefaultValues: muestra los valores por defecto
//--------------------------------------------------------------------------------------------
function insDefaultValues(){
//--------------------------------------------------------------------------------------------
//+ Se define la variable para almacenar el consecutivo más alto existente en el grid
    var llngMax = 0

//+ Se genera el número consecutivo de la imagen (el Nº consecutivo más alto +1)   
	for(var llngIndex = 0;llngIndex<top.opener.marrArray.length;llngIndex++)
	    if(eval(top.opener.marrArray[llngIndex].nCover)>llngMax)
	        llngMax = top.opener.marrArray[llngIndex].nCover

//+ Se asignan los valores a los campos de la página	
	with (self.document.forms[0]){
		nCover.value = ++llngMax;
	}
}

//% ShowSubSequence: muestra la subsecuencia para la cobertura en tratamiento
//--------------------------------------------------------------------------------------------
function ShowSubSequence(Index, nCover, nCovergen, nModulec){
//--------------------------------------------------------------------------------------------
	if(Index!=-1)
		ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sModule=Product&sProject=ProductSeq/CoverSeq&bAutomatic=false&sCodispl=DP034&nModulec=' + marrArray[Index].tcnModulec + '&nCovergen=' + marrArray[Index].nCovergen + '&nCover=' + marrArray[Index].nCover, 'CoverSeq', 750, 500, 'no', 'no', 20, 20, 'yes')  
	else
		ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sModule=Product&sProject=ProductSeq/CoverSeq&bAutomatic=true&sCodispl=DP034&nModulec=' + nModulec + '&nCovergen=' + nCovergen + '&nCover=' + nCover, 'CoverSeq', 750, 500, 'no', 'no', 20, 20, 'yes') 
}
</SCRIPT>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "DP033", "DP033.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
Response.Write(mobjValues.StyleSheet())
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP033" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%Response.Write(mobjValues.ShowWindowsName("DP033"))

'+Se llaman los procedimientos que definen el grid de coberturas
Call insDefineHeader()
mobjGrid.ActionQuery = Session("bQuery")

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP033Upd()
Else
	Call insPreDP033()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





