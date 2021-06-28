Imports System.Activities
Imports System.ComponentModel


Namespace Helpers
    ''' <summary>
    ''' Utility de soporte a los workflows
    ''' </summary>
    Friend NotInheritable Class Utility

        ''' <summary>
        ''' Devuelve la instancia de la clase context usada como parámetro del workflow.
        ''' </summary>
        ''' <param name="dataContext">Instancia del tipo dataContext del workflow</param>
        ''' <returns>En caso de existir el argumento context se retorna la instancia, en caso contrario se retorna nulo</returns>
        Friend Shared Function ExtractContextInstanceFromWorkflowDataContext(dataContext As WorkflowDataContext) As InMotionGIT.Common.Contracts.Context
            Return ExtractContextInstanceFromWorkflowDataContext(dataContext, Nothing)
        End Function

        ''' <summary>
        ''' Devuelve la instancia de la clase context usada como parámetro del workflow.
        ''' </summary>
        ''' <param name="dataContext">Instancia del tipo dataContext del workflow</param>
        ''' <param name="workflowProperties">Lista de propiedades del workflow a ser llenada por la función</param>
        ''' <returns>En caso de existir el argumento context se retorna la instancia, en caso contrario se retorna nulo</returns>
        Friend Shared Function ExtractContextInstanceFromWorkflowDataContext(dataContext As WorkflowDataContext, ByRef workflowProperties As Dictionary(Of String, Object)) As InMotionGIT.Common.Contracts.Context
            Dim result As InMotionGIT.Common.Contracts.Context = Nothing
            Dim value As Object = Nothing

            For Each propDescriptor As PropertyDescriptor In dataContext.GetProperties
                value = propDescriptor.GetValue(dataContext)
                If Not IsNothing(workflowProperties) Then
                    workflowProperties.Add(propDescriptor.Name, value)
                End If
                If Not IsNothing(value) AndAlso
                   String.Equals(value.GetType.FullName, "InMotionGIT.Common.Contracts.Context", StringComparison.CurrentCultureIgnoreCase) Then
                    result = TryCast(value, InMotionGIT.Common.Contracts.Context)
                End If
            Next

            Return result
        End Function

    End Class

End Namespace

