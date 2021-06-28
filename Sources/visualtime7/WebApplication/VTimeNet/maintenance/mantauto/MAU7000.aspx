<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import Namespace="System.Globalization" %>
<%@ Import namespace="eFunctions" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="eFunctions.Values" %>
<%@ Import namespace="eRemoteDB.Parameter" %>

<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Private mobjValues As New eFunctions.Values

    '- Se define la variable mobjGrid para el manejo del Grid de la ventana
    Private mobjGrid As eFunctions.Grid

    ''' <summary>
    ''' Definición de columnas del GRID
    ''' </summary>
    Private Sub insDefineHeader()
        mobjGrid = New eFunctions.Grid

        '+Se definen todas las columnas del Grid
        With mobjGrid.Columns
            .AddPossiblesColumn(0, GetLocalResourceObject("NVEHTYPE_Caption"), FieldName:="NVEHTYPE", TableName:="TABLE226", ValuesType:=eValuesType.clngWindowType, DefValue:="", NeedParam:=False, ComboSize:="1", OnChange:="InputOnChange(this)", Disabled:=False, MaxLength:=5, Alias_Renamed:=GetLocalResourceObject("NVEHTYPE_ToolTip"), CodeType:=eTypeCode.eNumeric, bAllowInvalid:=False, ShowDescript:=True, Descript:="", NotCache:=False, KeyField:="")
            .AddPossiblesColumn( 0, GetLocalResourceObject("NUSE_Caption"), FieldName:="NUSE", TableName:="TABLE6028", ValuesType:=eValuesType.clngComboType, DefValue:="", NeedParam:=False, ComboSize:="1", OnChange:="InputOnChange(this)", Disabled:=False, MaxLength:=5, Alias_Renamed:=GetLocalResourceObject("NUSE_ToolTip"), CodeType:=eTypeCode.eNumeric, bAllowInvalid:=False, ShowDescript:=True, Descript:="", NotCache:=False, KeyField:="")
            .AddNumericColumn( 0, GetLocalResourceObject("NOLDEST_Caption"), FieldName:="NOLDEST", Length:=2, DefValue:="",isRequired:=False, Alias_Renamed:=GetLocalResourceObject("NOLDEST_ToolTip"), ShowThousand:=False, DecimalPlaces:=0,OnChange:="InputOnChange(this)", Disabled:=False, bAllowNegativ:=False)
            .AddPossiblesColumn( 0,GetLocalResourceObject("NCURRENCY_Caption"), FieldName:="NCURRENCY", TableName:="TABLE11", ValuesType:=eValuesType.clngComboType, DefValue:="" , NeedParam:=False, ComboSize:="1", OnChange:="InputOnChange(this)", Disabled:=False, MaxLength:=5, Alias_Renamed:=GetLocalResourceObject("NCURRENCY_ToolTip"), CodeType:=eTypeCode.eNumeric, bAllowInvalid:=False, ShowDescript:=True, Descript:="", NotCache:=False, KeyField:="")
            .AddNumericColumn( 0, GetLocalResourceObject("NCAPITALMIN_Caption"), FieldName:="NCAPITALMIN", Length:=18, DefValue:="" ,isRequired:=False, Alias_Renamed:=GetLocalResourceObject("NCAPITALMIN_ToolTip"), ShowThousand:=True, DecimalPlaces:=0,OnChange:="InputOnChange(this)", Disabled:=False, bAllowNegativ:=False)

            .AddHiddenColumn("nConsecGrid", "0")
            .AddHiddenColumn("dEffectdateGrid", "0")
            .AddHiddenColumn("dNulldateCurrent", "")
        End With

        With mobjGrid
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
            .Codispl = "MAU7000"
            .Codisp = "MAU7000"
            .Top = 100
            .Height = 230
            .Width = 450
            .ActionQuery = mobjValues.ActionQuery
            .bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
            .Columns("Sel").GridVisible = Not .ActionQuery
            .Columns("NVEHTYPE").EditRecord = True

            .Columns("NVEHTYPE").Disabled = (Request.QueryString.Item("Action") = "Update")

            .sDelRecordParam = "cbeBranch=" & Request.QueryString.Item("cbeBranch") + "&valProduct=" & Request.QueryString.Item("valProduct") + "&DEFFECTDATE=" & Request.QueryString.Item("DEFFECTDATE") & "&nConsecGrid=' + marrArray[lintIndex].nConsecGrid + '" & "&NVEHTYPE=' + marrArray[lintIndex].NVEHTYPE + '" &  "&dEffectdateGrid=' + marrArray[lintIndex].dEffectdateGrid + '"
            .sEditRecordParam = "cbeBranch=" & Request.QueryString.Item("cbeBranch") + "&valProduct=" & Request.QueryString.Item("valProduct") + "&DEFFECTDATE=" & Request.QueryString.Item("DEFFECTDATE")

            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If

            .AddButton = True
            .DeleteButton = True
            .Columns("Sel").GridVisible = Not .ActionQuery
        End With
    End Sub

    ''' <summary>
    ''' Esta función se encarga de cargar los datos en la forma "Folder" 
    ''' </summary>
    Private Sub insPreMAU7000()
        With Request
            Dim rdb As New eRemoteDB.Execute

            rdb.SQL = "SELECT  ISSUECONTROLS.NVEHTYPE, ISSUECONTROLS.NUSE, ISSUECONTROLS.NOLDEST, ISSUECONTROLS.NCAPITALMIN, ISSUECONTROLS.NCURRENCY, ISSUECONTROLS.NCONSEC, ISSUECONTROLS.DNULLDATE, ISSUECONTROLS.NBRANCH, ISSUECONTROLS.NPRODUCT, ISSUECONTROLS.DEFFECTDATE FROM ISSUECONTROLS ISSUECONTROLS  WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND DEFFECTDATE <= :DEFFECTDATE AND (DNULLDATE  IS NULL OR  DNULLDATE  > :DEFFECTDATE) ORDER BY NVEHTYPE, NUSE"
            rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
            rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
            rdb.Parameters.Add("DEFFECTDATE", mobjValues.StringToDate(.QueryString.Item("DEFFECTDATE")), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)

            With mobjGrid
                If rdb.Run(True) Then
                    Do While Not rdb.EOF
                        .Columns("NVEHTYPE").DefValue = rdb.FieldToClass("NVEHTYPE")

                        .Columns("NUSE").DefValue = rdb.FieldToClass("NUSE")
                        .Columns("NOLDEST").DefValue = rdb.FieldToClass("NOLDEST")
                        .Columns("NCAPITALMIN").DefValue = rdb.FieldToClass("NCAPITALMIN")

                        .Columns("NCURRENCY").DefValue = rdb.FieldToClass("NCURRENCY")
                        .Columns("nConsecGrid").DefValue = rdb.FieldToClass("NCONSEC")
                        .Columns("dEffectdateGrid").DefValue = rdb.FieldToClass("DEFFECTDATE")
                        .Columns("dNulldateCurrent").DefValue = rdb.FieldToClass("DNULLDATE")

                        Response.Write(.DoRow)
                        rdb.RNext()
                    Loop
                    rdb.RCloseRec()
                End If
                Response.Write(.CloseTable())
            End With
        End With
    End Sub

    ''' <summary>
    ''' Se define esta funcion para contruir el contenido de la ventana UPD de los archivos de datos particulares
    ''' </summary>
    Private Sub insPreMAU7000Upd()
        With Request
            If .QueryString.Item("Action") = "Del" Then
                Dim lblnPost As Boolean
                Dim lstrMessage As String = String.Empty
                Dim rdb As New eRemoteDB.Execute

                If String.IsNullOrEmpty(lstrMessage) Then
                    rdb = New eRemoteDB.Execute

                    If mobjValues.StringToType(.QueryString.Item("DEFFECTDATE"), eFunctions.Values.eTypeData.etdDate) = mobjValues.StringToType(.QueryString.Item("dEffectdateGrid"), eFunctions.Values.eTypeData.etdDate) Then
                        rdb.SQL = "DELETE FROM ISSUECONTROLS WHERE NVEHTYPE = :NVEHTYPE AND NCONSEC = :nConsecGrid  AND NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND DEFFECTDATE = :dEffectdateGrid "
                    Else
                        rdb.SQL = "UPDATE ISSUECONTROLS SET DNULLDATE = :DEFFECTDATE WHERE NVEHTYPE = :NVEHTYPE AND NCONSEC = :nConsecGrid AND NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND DEFFECTDATE = :dEffectdateGrid AND DNULLDATE IS NULL"
                        rdb.Parameters.Add("DEFFECTDATE", mobjValues.StringToType(.QueryString.Item("DEFFECTDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 0, 0, 0, eRmtDataAttrib.rdbParamNullable)
                    End If

                    rdb.Parameters.Add("NVEHTYPE", mobjValues.StringToType(.QueryString.Item("NVEHTYPE"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("nConsecGrid", mobjValues.StringToType(.QueryString.Item("nConsecGrid"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("dEffectdateGrid", mobjValues.StringToDate(.QueryString.Item("dEffectdateGrid")), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 0, 0, 0, eRmtDataAttrib.rdbParamNullable)

                    lblnPost = rdb.Run(False)
                    Response.Write(mobjValues.ConfirmDelete())
                Else
                    Response.Write(lstrMessage)
                End If
            End If

            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "MAU7000_val.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
        End With
    End Sub

</script>
<%      
	Response.Expires = -1441
%>
<script type="text/javascript" language="JavaScript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
<script type="text/javascript" language="JavaScript">
    function InputOnChange(field) {
        switch (field.name) {   
        }    
    }
</script>
<html>
<head>
<%
    mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery))
    Response.Write(mobjValues.StyleSheet())
    Response.Write("<script language='JavaScript'>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</script>")
    If Request.QueryString.Item("Type") <> "PopUp" Then
        With New eFunctions.Menues
            Response.Write(.setZone(2, "MAU7000", "MAU7000.aspx"))
        End With
    End If
%>
</head>
<body onunload="closeWindows();">
    <form method="post" id="FORM" action="MAU7000_val.aspx?sZone=2">
		<%
			Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
			Call insDefineHeader()
			If Request.QueryString.Item("Type") <> "PopUp" Then
				Call insPreMAU7000()
			Else
				Call insPreMAU7000Upd()
			End If
		%>	  
    </form>
</body>
</html>