Option Strict Off
Option Explicit On
Public Class Business_Functs
	'**+Objective: Class that supports the general policy functions
	'**+Version: $$Revision: 1 $
	'+Objetivo: Clase que soporta funciones general de la poliza:
	'+Version: $$Revision: 1 $
	
	'**%Objective: Return the scomplcod field for a Particular data table  : nBusinessty + nCommenGrp + nCodKind
	'%Objetivo: Retorn el campo scomplcod para una tabla de Datos Particulares  : nBusinessty + nCommenGrp + nCodKind.
	Public Function calComplCode(ByVal nCodkind As Short, ByVal nBusinessty As Short, ByVal nCommergrp As Short) As String
		
		Dim lstrBusinessty As String
		Dim lstrCommergrp As String
		Dim lstrCodKind As String
		
        '+ Cadena que guarda la estructura del codigo de giro de negocio
		
		lstrBusinessty = CStr(nBusinessty)
		lstrCommergrp = "00" & CStr(nCommergrp)
		lstrCommergrp = Right(lstrCommergrp, 3)
		lstrCodKind = "0" & CStr(nCodkind)
		lstrCodKind = Right(lstrCodKind, 2)
		
		calComplCode = lstrBusinessty & lstrCommergrp & lstrCodKind
		
		
		Exit Function
	End Function
	
	Public Function getBusinessty(ByVal sComplCod As String) As Short

        If Not String.IsNullOrEmpty(sComplCod) Then
            getBusinessty = CShort(Mid(sComplCod, 1, 1))
        End If


        Exit Function
    End Function
	
	Public Function getCommergrp(ByVal sComplCod As String) As Short
		
        If Not String.IsNullOrEmpty(sComplCod) Then
            getCommergrp = CShort(Mid(sComplCod, 2, 3))
        End If
		
		Exit Function
		
	End Function
	
	Public Function getCodkind(ByVal sComplCod As String) As Short
		
        If Not String.IsNullOrEmpty(sComplCod) Then
            getCodkind = CShort(Mid(sComplCod, 5, 2))
        End If

		Exit Function
		
	End Function
End Class











