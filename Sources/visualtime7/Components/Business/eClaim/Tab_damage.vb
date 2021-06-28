Option Strict Off
Option Explicit On
Public Class Tab_damage
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_damage.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Defines the principal properties of the corresponding class to the Claim_Dama table
	'**- The key field correspond to nClaim, nCase_num, n_Deman_type, nDmage_cod
	'- Se definen las propiedades principales de la clase correspondientes a la tabla Claim_Dama
	'- El campo llave corresponde a nClaim, nCase_num, nDeman_type, nDamage_cod
	
	'Column_name                           Type                           Length Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	'------------------------------------- ------------------------------ ------ ----- ----- ----------------------------------- ----------------------------------- -----------------------------------
	Public nBranch As Object 'Smallint                      no     2     5     0     no                                  (n/a)                               (n/a)
	Public nDamage_cod As Object 'Smallint                      no     2     5     0     no                                  (n/a)                               (n/a)
	Public sDescript As Object 'char                          no     30                      yes                            no                                  yes
	Public sShort_des As Object 'char                          no     12                      yes                            no                                  yes
	Public sStatregt As Object 'char                          no     1                       yes                            no                                  yes
	Public nUsercode As Object 'Smallint                      no     2     5     0     yes                                 (n/a)                               (n/a)
	
	Public nQuantity As Integer
	Public nSel As Integer
End Class






