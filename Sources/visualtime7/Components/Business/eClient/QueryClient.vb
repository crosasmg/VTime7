Option Strict Off
Option Explicit On
Public Class QueryClient
	'%-------------------------------------------------------%'
	'% $Workfile:: QueryClient.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	'**+Collection elements
	'**+ The key fields are sClient, sCliename, DescSex, dBirthdat
	'+ Elementos de la colección...
	'+ Los campos corresponden a sClient, sCliename, DescSex, dBirthdat
	
	Public sClient As String '+ Codigo del cliente
	Public sDigit As String '+ Digito verificador.
	Public sClieName As String '+ Nombre Completo del cliente
	Public dBirthdat As Date '+ Fecha de nacimiento
	Public sSexclien As String '+ Codigo del sexo
	Public sFirstName As String '+ Nombre del cliente
	Public sLastName As String '+ Apellido Paterno
	Public sLastName2 As String '+ Apellido Materno
	
	'**%Class_Initialize: This function controls the open of the class
	'% Class_Initialize: el objetivo de esta rutina es la de controlar la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Me.sClient = String.Empty
		Me.sClieName = String.Empty
		Me.dBirthdat = eRemoteDB.Constants.dtmNull
		Me.sSexclien = String.Empty
		Me.sFirstName = String.Empty
		Me.sLastName = String.Empty
		Me.sLastName2 = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






