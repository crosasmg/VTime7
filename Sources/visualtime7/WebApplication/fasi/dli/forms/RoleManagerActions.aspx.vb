﻿'---------------------------------------------------------------------------------------------------
' <generated>
'     This code was generated by Form Designer v7.3.39.1 at 2020-04-20 03:56:12 PM model release 19, Form Generator v1.0.37.52
'     
'     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
' </generated>
'---------------------------------------------------------------------------------------------------
      
#Region "using"

Imports System.Data
Imports System.Globalization
Imports System.Net
Imports System.Runtime.Serialization
Imports System.Web.Script.Services
Imports System.Web.Services
Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.Common.Proxy
Imports InMotionGIT.FrontOffice.Support

#End Region

Namespace dli.forms

    Public Class RoleManagerActions
        Inherits System.Web.UI.Page

#Region "Actions Methods"

       <WebMethod(EnableSession:=True)>
        Public Shared Function Initialization(id As String, urlid As String, fromid As String) As InMotionGIT.FrontOffice.Support.DataType.ServerActionResult
            
            Dim instance As New EntryData With {.InstanceFormId = id}
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ServerActionResult
            Dim currentAction As String = String.Empty
            
            
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("Administrador")
                
                If id.IsEmpty AndAlso urlid.IsEmpty Then
                    instance.InstanceFormId = System.Guid.NewGuid().ToString
                  
                
                Else
                    instance.InstanceFormId = id.IfEmpty(urlid)
                End If             
             
                With resultData
                    .Success = True
                    .Data = New With {.Instance = instance, .LookUps = Nothing}
                End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessServerAction(ex, "RoleManager", "Initialization", currentAction)
            End Try
            
            Return resultData
        End Function
  

        <WebMethod()>
        Public Shared Function Clean5e21d681d528473c81c3aec7253ef516() As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = Nothing
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("Administrador")
InMotionGIT.Common.Helpers.Caching.Clean() 
 

                With resultData
                    .Success = True
                    .Data = response
                End With
                
            Catch ex As Exception            
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "RoleManager", "Clean5e21d681d528473c81c3aec7253ef516", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function Clean6b4b847d024f427ba5aea4397cdf59df() As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = Nothing
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("Administrador")
InMotionGIT.Common.Helpers.Caching.Clean() 
 

                With resultData
                    .Success = True
                    .Data = response
                End With
                
            Catch ex As Exception            
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "RoleManager", "Clean6b4b847d024f427ba5aea4397cdf59df", String.Empty)
            End Try
            
            Return resultData
        End Function

        <WebMethod()>
        Public Shared Function Clean8a7886f19915466caf50a1c5e03590bb() As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True, .Count = 0}
            Dim response As Object = Nothing
            
            Try
               InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("Administrador")
InMotionGIT.Common.Helpers.Caching.Clean() 
 

                With resultData
                    .Success = True
                    .Data = response
                End With
                
            Catch ex As Exception            
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "RoleManager", "Clean8a7886f19915466caf50a1c5e03590bb", String.Empty)
            End Try
            
            Return resultData
        End Function


#End Region

#Region "Lookups Web Methods"


#End Region

#Region "Form Actions"


#End Region





#Region "Common Utilities"


#End Region

#Region "Form Contracts"

        <Serializable()>
        <DataContract()>
        Public Class EntryData

            <DataMember()> Public Property InstanceFormId As String
            <DataMember()> Public Property RoleListId As System.Int32
            <DataMember()> Public Property RoleListName As System.String
            <DataMember()> Public Property RoleListSecurityLevel As System.Int32
            <DataMember()> Public Property RoleListIsBackOfficeSource As System.Boolean
            <DataMember()> Public Property Role_Role As List(Of Role_RoleItem)

        End Class

        <Serializable()>
        <DataContract()>
        Public Class Role_RoleItem

            <DataMember()> Public Property Id As System.Int32
            <DataMember()> Public Property Name As System.String
            <DataMember()> Public Property SecurityLevel As System.Int32
            <DataMember()> Public Property IsBackOfficeSource As System.Boolean

        End Class


#End Region

    End Class

End Namespace

