Imports Newtonsoft.Json.Linq
Public Class cStudentOO
    Inherits cStudentBasis
    Public Function OOnode() As TreeNode
        'Data uit OO
        Dim sOO As cStudentOO = Me.GetStudentClassUitOoStudentNummer(Me.StudentNummer, False)
        If IsNothing(sOO) Then
            Return New TreeNode("Bestaat niet in OO")
        End If
        Dim ndOO As New TreeNode(sOO.StudentNummer & " - OnderwijsOnline")
        Dim ndNawOO As TreeNode = New TreeNode(sOO.VolledigeNaam)

        Dim ndOpleidingenOO As New TreeNode("Opleiding")
        Dim ndKlassenOO As New TreeNode("Klassen")
        For Each kv In Me.GroepsDeelnames
            If BestaatGroepInOO(kv.Value.GroepsCode) Then
                ndKlassenOO.Nodes.Add(kv.Value.GroepsCode & " is aanwezig in OO")
            End If
        Next

        For Each kv In Me.Opleidingen
            'controleren of opleiding in OO aanwezig is
        Next

        ndOO.Nodes.Add(ndNawOO)
        ndOO.Nodes.Add(ndKlassenOO)
        ndOO.Nodes.Add(ndOpleidingenOO)


        Return ndOO
    End Function
End Class
