﻿'---------------------------------------------------------------------------------------------------
' <generated>
'     This code was generated by Form Designer v7.1.215.1 at 2019-02-04 09:47:56 AM model release 2, Form Generator v1.0.34.9
'     
'     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
' </generated>
'---------------------------------------------------------------------------------------------------
      
#Region "using"

Imports System.Data
Imports System.Globalization
Imports System.Runtime.Serialization
Imports System.Web.Script.Services
Imports System.Web.Services
Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.Common.Proxy
Imports InMotionGIT.FrontOffice.Support

#End Region

Namespace dli.forms

    Public Class NSF2501AActions
        Inherits System.Web.UI.Page

#Region "Actions Methods"
   
        <WebMethod()>
        Public Shared Function Grupo_Acceso_GridTblDataLoad(filter As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = New With {.Id_Grupo_Acceso = 0, .Descripcion = String.Empty, .Descripcion_Corta = String.Empty, .Estado_Registro = String.Empty, .CreationDate = Date.MinValue, .CreatorUserCode = 0, .UpdateDate = Date.MinValue, .UpdateUserCode = 0}
            Dim selectDataTableItem As DataTable
            Dim responseList As New List(Of Object)

            Try

                With New DataManagerFactory("SELECT GRUPO_ACCESO.ID_GRUPO_ACCESO, GRUPO_ACCESO.DESCRIPCION, GRUPO_ACCESO.DESCRIPCION_CORTA, GRUPO_ACCESO.ESTADO_REGISTRO, GRUPO_ACCESO.CREATIONDATE, GRUPO_ACCESO.CREATORUSERCODE, GRUPO_ACCESO.UPDATEDATE, GRUPO_ACCESO.UPDATEUSERCODE FROM GRUPO_ACCESO GRUPO_ACCESO  ORDER BY Grupo_Acceso.Descripcion ASC", "Grupo_Acceso", "Linked.Seguridad")

                    selectDataTableItem = .QueryExecuteToTable(True)
                End With
                With selectDataTableItem
                    If Not IsNothing(.Rows) AndAlso .Rows.Count > 0 Then
                        For Each itemData As DataRow In .Rows
                            response = New With {.Id_Grupo_Acceso = itemData.NumericValue("ID_GRUPO_ACCESO"), .Descripcion = itemData.StringValue("DESCRIPCION"), .Descripcion_Corta = itemData.StringValue("DESCRIPCION_CORTA"), .Estado_Registro = itemData.StringValue("ESTADO_REGISTRO"), .CreationDate = itemData.DateTimeValue("CREATIONDATE"), .CreatorUserCode = itemData.NumericValue("CREATORUSERCODE"), .UpdateDate = itemData.DateTimeValue("UPDATEDATE"), .UpdateUserCode = itemData.NumericValue("UPDATEUSERCODE")}

                            responseList.Add(response)
                        Next

                        With resultData
                            .Count = responseList.Count
                            .Data = responseList
                        End With
                    End If
                End With

            Catch ex As Exception
                LogHandler.ErrorLog("NSF2501A", "Grupo_Acceso_GridTblDataLoad", ex)

                With resultData
                    .Success = False
                    .Reason = String.Format(CultureInfo.InvariantCulture, "{0} (Grupo_Acceso_GridTblDataLoad)", ex.Message)
                End With
            End Try

            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function Grupo_Acceso_GridTblDataCount(filter As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}

            Try
                Dim recordCount As Integer = 0                

                With New DataManagerFactory("SELECT COUNT(*) FROM (SELECT GRUPO_ACCESO.ID_GRUPO_ACCESO, GRUPO_ACCESO.DESCRIPCION, GRUPO_ACCESO.DESCRIPCION_CORTA, GRUPO_ACCESO.ESTADO_REGISTRO, GRUPO_ACCESO.CREATIONDATE, GRUPO_ACCESO.CREATORUSERCODE, GRUPO_ACCESO.UPDATEDATE, GRUPO_ACCESO.UPDATEUSERCODE FROM GRUPO_ACCESO GRUPO_ACCESO  ORDER BY Grupo_Acceso.Descripcion ASC)", "Grupo_Acceso", "Linked.Seguridad")

                    recordCount = .QueryExecuteScalarToInteger()
                End With

                With resultData
                    .Count = recordCount
                    .Data = recordCount
                End With

            Catch ex As Exception
                LogHandler.ErrorLog("NSF2501A", "Grupo_Acceso_GridTblDataCount", ex)

                With resultData
                    .Success = False
                    .Reason = String.Format(CultureInfo.InvariantCulture, "{0} (Grupo_Acceso_GridTblDataCount)", ex.Message)
                End With
            End Try

            Return resultData
        End Function
        <WebMethod()>
        Public Shared Function Grupo_Acceso_GridInsertCommandActionGrupo_Acceso(GRUPO_ACCESOID_GRUPO_ACCESO1 As Decimal, GRUPO_ACCESODESCRIPCION2 As String, GRUPO_ACCESODESCRIPCION_CORTA3 As String, GRUPO_ACCESOESTADO_REGISTRO4 As String, GRUPO_ACCESOCREATORUSERCODE5 As Decimal, GRUPO_ACCESOUPDATEUSERCODE7 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}

            Try
                With New DataManagerFactory("INSERT INTO Grupo_Acceso (ID_GRUPO_ACCESO, DESCRIPCION, DESCRIPCION_CORTA, ESTADO_REGISTRO, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:GRUPO_ACCESOID_GRUPO_ACCESO1, @:GRUPO_ACCESODESCRIPCION2, @:GRUPO_ACCESODESCRIPCION_CORTA3, @:GRUPO_ACCESOESTADO_REGISTRO4, @:GRUPO_ACCESOCREATIONDATE4, @:GRUPO_ACCESOCREATORUSERCODE5, @:GRUPO_ACCESOUPDATEDATE6, @:GRUPO_ACCESOUPDATEUSERCODE7)", "Grupo_Acceso", "Linked.Seguridad")
                    .AddParameter("GRUPO_ACCESOID_GRUPO_ACCESO1", DbType.Decimal, 0, False, GRUPO_ACCESOID_GRUPO_ACCESO1)
                    .AddParameter("GRUPO_ACCESODESCRIPCION2", DbType.AnsiString, 0, (GRUPO_ACCESODESCRIPCION2 = String.Empty), GRUPO_ACCESODESCRIPCION2)
                    .AddParameter("GRUPO_ACCESODESCRIPCION_CORTA3", DbType.AnsiString, 0, (GRUPO_ACCESODESCRIPCION_CORTA3 = String.Empty), GRUPO_ACCESODESCRIPCION_CORTA3)
                    .AddParameter("GRUPO_ACCESOESTADO_REGISTRO4", DbType.AnsiStringFixedLength, 0, (GRUPO_ACCESOESTADO_REGISTRO4 = String.Empty), GRUPO_ACCESOESTADO_REGISTRO4)
                    .AddParameter("GRUPO_ACCESOCREATIONDATE4", DbType.DateTime, 0, False, Date.Now)
                    .AddParameter("GRUPO_ACCESOCREATORUSERCODE5", DbType.Decimal, 0, False, GRUPO_ACCESOCREATORUSERCODE5)
                    .AddParameter("GRUPO_ACCESOUPDATEDATE6", DbType.DateTime, 0, False, Date.Now)
                    .AddParameter("GRUPO_ACCESOUPDATEUSERCODE7", DbType.Decimal, 0, False, GRUPO_ACCESOUPDATEUSERCODE7)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                LogHandler.ErrorLog("NSF2501A", "Grupo_Acceso_GridInsertCommandActionGrupo_Acceso", ex)

                With resultData
                    .Success = False
                    .Reason = String.Format(CultureInfo.InvariantCulture, "{0} (Grupo_Acceso_GridInsertCommandActionGrupo_Acceso)", ex.Message)
                End With
            End Try

            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function Grupo_Acceso_GridUpdateCommandActionGrupo_Acceso(GRUPO_ACCESODESCRIPCION1 As String, GRUPO_ACCESODESCRIPCION_CORTA2 As String, GRUPO_ACCESOESTADO_REGISTRO3 As String, GRUPO_ACCESOUPDATEUSERCODE4 As Decimal, GrupoAccesoIdGrupoAcceso6 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}

            Try
                With New DataManagerFactory("UPDATE Grupo_Acceso SET DESCRIPCION = @:GRUPO_ACCESODESCRIPCION1, DESCRIPCION_CORTA = @:GRUPO_ACCESODESCRIPCION_CORTA2, ESTADO_REGISTRO = @:GRUPO_ACCESOESTADO_REGISTRO3, UPDATEDATE = @:GRUPO_ACCESOUPDATEDATE3, UPDATEUSERCODE = @:GRUPO_ACCESOUPDATEUSERCODE4 WHERE GRUPO_ACCESO.ID_GRUPO_ACCESO = @:GRUPO_ACCESOID_GRUPO_ACCESO6", "Grupo_Acceso", "Linked.Seguridad")
                    .AddParameter("GRUPO_ACCESODESCRIPCION1", DbType.AnsiString, 0, (GRUPO_ACCESODESCRIPCION1 = String.Empty), GRUPO_ACCESODESCRIPCION1)
                    .AddParameter("GRUPO_ACCESODESCRIPCION_CORTA2", DbType.AnsiString, 0, (GRUPO_ACCESODESCRIPCION_CORTA2 = String.Empty), GRUPO_ACCESODESCRIPCION_CORTA2)
                    .AddParameter("GRUPO_ACCESOESTADO_REGISTRO3", DbType.AnsiStringFixedLength, 0, (GRUPO_ACCESOESTADO_REGISTRO3 = String.Empty), GRUPO_ACCESOESTADO_REGISTRO3)
                    .AddParameter("GRUPO_ACCESOUPDATEDATE3", DbType.DateTime, 0, False, Date.Now)
                    .AddParameter("GRUPO_ACCESOUPDATEUSERCODE4", DbType.Decimal, 0, False, GRUPO_ACCESOUPDATEUSERCODE4)
                    .AddParameter("GRUPO_ACCESOID_GRUPO_ACCESO6", DbType.Decimal, 0, False, GrupoAccesoIdGrupoAcceso6)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                LogHandler.ErrorLog("NSF2501A", "Grupo_Acceso_GridUpdateCommandActionGrupo_Acceso", ex)

                With resultData
                    .Success = False
                    .Reason = String.Format(CultureInfo.InvariantCulture, "{0} (Grupo_Acceso_GridUpdateCommandActionGrupo_Acceso)", ex.Message)
                End With
            End Try

            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function Grupo_Acceso_GridDeleteCommandActionGrupo_Acceso(GrupoAccesoIdGrupoAcceso1 As Decimal) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}

            Try
                With New DataManagerFactory("DELETE FROM Grupo_Acceso WHERE GRUPO_ACCESO.ID_GRUPO_ACCESO = @:GRUPO_ACCESOID_GRUPO_ACCESO1", "Grupo_Acceso", "Linked.Seguridad")
                    .AddParameter("GRUPO_ACCESOID_GRUPO_ACCESO1", DbType.Decimal, 0, False, GrupoAccesoIdGrupoAcceso1)
 
                    .CommandExecute()
              End With

            Catch ex As Exception
                LogHandler.ErrorLog("NSF2501A", "Grupo_Acceso_GridDeleteCommandActionGrupo_Acceso", ex)

                With resultData
                    .Success = False
                    .Reason = String.Format(CultureInfo.InvariantCulture, "{0} (Grupo_Acceso_GridDeleteCommandActionGrupo_Acceso)", ex.Message)
                End With
            End Try

            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function Grupo_Acceso_GridSelectCommandActionGrupo_Acceso() As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = New With {.Result = 0}

            Try

                With New DataManagerFactory("Select MAX(GRUPO_ACCESO.ID_GRUPO_ACCESO) ID_GRUPO_ACCESO FROM GRUPO_ACCESO GRUPO_ACCESO ", "Grupo_Acceso", "Linked.Seguridad")

                    response.Result = .QueryExecuteScalarToInteger()
                End With
                With resultData
                    .Count = 1
                    .Data = response
                End With

            Catch ex As Exception
                LogHandler.ErrorLog("NSF2501A", "Grupo_Acceso_GridSelectCommandActionGrupo_Acceso", ex)

                With resultData
                    .Success = False
                    .Reason = String.Format(CultureInfo.InvariantCulture, "{0} (Grupo_Acceso_GridSelectCommandActionGrupo_Acceso)", ex.Message)
                End With
            End Try

            Return resultData
        End Function


#End Region

#Region "Lookups Web Methods"


        <WebMethod(EnableSession:=True)>
        Public Shared Function LookUpForEstado_Registro(id As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            Dim result As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True}
            Dim resultData As DataTable = Nothing
            Dim newLookupList As New List(Of InMotionGIT.Common.DataType.LookUpValue)
            
            
            Try
                Dim UserInfo As New InMotionGIT.Membership.Providers.MemberContext
                With New DataManagerFactory(String.Format(CultureInfo.CurrentCulture,
                                                          "SELECT  TRIM(ENUMRECORDSTATUS.RECORDSTATUS) RECORDSTATUS, ETRANRECORDSTATUS.DESCRIPTION FROM ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ENUMRECORDSTATUS.RECORDSTATUSCODE = '{0}' AND ETRANRECORDSTATUS.LANGUAGEID = {1}  ORDER BY ETranRecordStatus.Description ASC", "1", InMotionGIT.FASI.Support.Handlers.LanguageHandler.LanguageId()), 
                                            "EnumRecordStatus", "Linked.Common")

                    
                    resultData = .QueryExecuteToTable(True)
                End With

                If Not IsNothing(resultData) Then
                    For Each item As DataRow In resultData.Rows
                        newLookupList.Add(New InMotionGIT.Common.DataType.LookUpValue With {.Code = item.StringValue("RecordStatus"),
                                                                                            .Description = item.StringValue("Description")})
                    Next
                End If

                result.Data = newLookupList

            Catch ex As Exception
                LogHandler.ErrorLog("NSF2501A", "LookUpForEstado_Registro", ex)

                With result
                    .Success = False
                    .Reason = String.Format(CultureInfo.InvariantCulture, "{0} ({1})", ex.Message, "LookUpForEstado_Registro")
                End With
            End Try
            Return result
        End Function

#End Region

#Region "Form Actions"


#End Region





#Region "Common Utilities"

        Private Shared Sub SetDefaultValuesFromQueryString(formData As EntryData)

        End Sub	


#End Region

#Region "Form Contracts"

        <Serializable()>
        <DataContract()>
        Public Class EntryData

            <DataMember()> Public Property InstanceFormId As String
            <DataMember()> Public Property Grupo_AccesoCollectionId_Grupo_Acceso As System.Double
            <DataMember()> Public Property Grupo_AccesoCollectionDescripcion As System.String
            <DataMember()> Public Property Grupo_AccesoCollectionDescripcion_Corta As System.String
            <DataMember()> Public Property Grupo_AccesoCollectionEstado_Registro As System.String
            <DataMember()> Public Property Grupo_AccesoCollectionCreationDate As System.DateTime
            <DataMember()> Public Property Grupo_AccesoCollectionCreatorUserCode As System.Double
            <DataMember()> Public Property Grupo_AccesoCollectionUpdateDate As System.DateTime
            <DataMember()> Public Property Grupo_AccesoCollectionUpdateUserCode As System.Double
            <DataMember()> Public Property Grupo_Acceso_Grid_Grupo_Acceso_Item As List(Of Grupo_Acceso_Grid_Grupo_Acceso_ItemItem)

        End Class

        <Serializable()>
        <DataContract()>
        Public Class Grupo_Acceso_Grid_Grupo_Acceso_ItemItem

            <DataMember()> Public Property Id_Grupo_Acceso As System.Double
            <DataMember()> Public Property Descripcion As System.String
            <DataMember()> Public Property Descripcion_Corta As System.String
            <DataMember()> Public Property Estado_Registro As System.String
            <DataMember()> Public Property CreationDate As System.DateTime
            <DataMember()> Public Property CreatorUserCode As System.Double
            <DataMember()> Public Property UpdateDate As System.DateTime
            <DataMember()> Public Property UpdateUserCode As System.Double

        End Class


#End Region

    End Class

End Namespace