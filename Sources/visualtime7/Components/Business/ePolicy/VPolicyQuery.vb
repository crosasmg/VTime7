Option Strict Off
Option Explicit On
Public Class VPolicyQuery
	'%-------------------------------------------------------%'
	'% $Workfile:: VPolicyQuery.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:06p                                $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Estructura de la vista VPolicyQuery_Certype[n], donde n es el tipo de registro (sCertype)
	'+ a utilizar en la búsqueda
	
	'+ Property            Type
	'+ ------------------- ----------
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy As Double
	Public nCertif As Double
	Public sStatusva As String
	Public dStartdate As Date
	Public dExpirdat As Date
	Public dDate_Origi As Date
	Public nDigit As Short
	Public sPolitype As String
	Public sClientC As String
	Public sClientA As String
	Public sClient As String
    Public sRegist As String
    Public sAutoDigit As String

	
	'+ Propiedades auxiliares
	
	'- Compañía en donde se encuentra la cotización/póliza/propuesta
	Public nCompany As Integer
End Class






