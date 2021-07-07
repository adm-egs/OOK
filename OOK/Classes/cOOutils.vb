Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Net
Imports System.IO
'Imports System.Web.Script.Serialization
Public Class cOOutils


    'Function GetStudentDataVanOO(sStudentNummer As String) As cStudent
    '    'gegevens van 1 student opvragen
    '    Dim bolKlassen As Boolean = False
    '    Dim bolOpleidingen As Boolean = False

    '    Dim rd As OracleDataReader = dbOsiris.oracleQueryUitvoeren(i.SqlStudentOsirisOpvragen(sStudentNummer))
    '    If rd.HasRows = False Then
    '        rd.Close()
    '        l.LOGTHIS("student " & sStudentNummer & " niet gevonden ")
    '        Return Nothing
    '    End If

    '    ' functie vraagt de data van 1 student op
    '    Dim s As New cStudent
    '    Try

    '        While rd.Read
    '            'studentobject vullen

    '            With s
    '                .Id = dbOsiris.oraSafeGetDecimal(rd, "ID")
    '                .Username = .Id
    '                .Roepnaam = dbOsiris.oraSafeGetString(rd, "Roepnaam")
    '                .Tussenvoegsels = dbOsiris.oraSafeGetString(rd, "Tussenvoegsels")
    '                .Achternaam = dbOsiris.oraSafeGetString(rd, "Achternaam")
    '                .Geslacht = dbOsiris.oraSafeGetString(rd, "Geslacht")
    '                .geboorteDatum = dbOsiris.oraGetSafeDate(rd, "Geboortedatum")
    '                .Initialen = dbOsiris.oraSafeGetString(rd, "initialen")
    '                .Mobiel_Nummer = dbOsiris.oraSafeGetString(rd, "mobiel_telefoonnummer")
    '                .WerkMailAdres = dbOsiris.oraSafeGetString(rd, "e_mail_adres")
    '                .Voornamen = "" 'dbOsiris.oraSafeGetString(rd, "voornamen") -> was ook zijn in de vorige versie, niet gevuld
    '                .PrimaireLocatieCode = dbOsiris.oraSafeGetString(rd, "PrimaireLocatieCode")
    '                .PrimaireLocatieNaam = dbOsiris.oraSafeGetString(rd, "PrimaireLocatieNaam")

    '            End With

    '            Return s

    '        End While
    '    Catch ex As Exception
    '        l.LOGTHIS("Fout bij opvragen studenten : " & ex.Message)
    '        Return Nothing
    '    End Try

    '    bolOpleidingen = GetAlleStudentOpleidingenOsiris()
    '    bolKlassen = GetStudentKlassen()

    '    If bolOpleidingen = True And bolKlassen = True Then
    '        Return s
    '    Else
    '        Return Nothing
    '    End If

    'End Function
    'Private Function GetAlleStudentOpleidingenOsiris() As Boolean
    '    'alle opleidingen van de studenten opvragen en deze aanvullen bij de studenten uit de dictOsirisStudenten
    '    l.LOGTHIS("Opleidingen bij de studenten opvragen en invullen")
    '    Dim rd As OracleDataReader = dbOsiris.oracleQueryUitvoeren(i.SqlStudentOpleidingenAlleStudenten)
    '    If rd.HasRows = False Then
    '        rd.Close()
    '        l.LOGTHIS("Geen studenten beschikbaar")
    '        Return False
    '    End If
    '    Try
    '        Dim studentNummer As String = ""
    '        While rd.Read
    '            Dim opl As New cOpleiding
    '            studentNummer = dbOsiris.oraSafeGetDecimal(rd, "studentnummer")
    '            opl.Code = dbOsiris.oraSafeGetString(rd, "Opleidingscode")
    '            opl.Naam = dbOsiris.oraSafeGetString(rd, "Opleidingsnaam")
    '            opl.Startdatum = dbOsiris.oraGetSafeDate(rd, "Startdatum")
    '            opl.Einddatum = dbOsiris.oraGetSafeDate(rd, "Einddatum")
    '            opl.Einddatum_werkelijk = dbOsiris.oraGetSafeDate(rd, "Einddatum_werkelijk")
    '            opl.TeamCode = dbOsiris.oraSafeGetString(rd, "Teamcodes")
    '            opl.TeamNaam = dbOsiris.oraSafeGetString(rd, "Teamnamen")

    '            'aanvullende velden bij de student toevoegen
    '            If dictOsirisStudenten.ContainsKey(studentNummer) Then
    '                'opleidingen bij de student toevoegen
    '                If Not dictOsirisStudenten(studentNummer).Opleidingen.ContainsKey(opl.UniqueKey) Then
    '                    dictOsirisStudenten(studentNummer).Opleidingen.Add(opl.UniqueKey, opl)
    '                End If
    '                'teams toevoegen
    '                dictOsirisStudenten(studentNummer).AddTeam(opl.TeamNaam, opl.TeamNaam)

    '            Else
    '                l.LOGTHIS("student niet gevonden voor opleiding: " & studentNummer)
    '            End If

    '            'teams toevoegen bij de student zelf

    '        End While
    '    Catch ex As Exception
    '        Return False

    '    End Try
    '    Return True
    'End Function
    'Private Function GetStudentOpleidingenOsiris1STudent() As Boolean
    '    'alle opleidingen van de studenten opvragen en deze aanvullen bij de studenten uit de dictOsirisStudenten
    '    l.LOGTHIS("Opleidingen bij de studenten opvragen en invullen")
    '    Dim rd As OracleDataReader = dbOsiris.oracleQueryUitvoeren(i.SqlStudentOpleidingenAlleStudenten)
    '    If rd.HasRows = False Then
    '        rd.Close()
    '        l.LOGTHIS("Geen studenten beschikbaar")
    '        Return False
    '    End If
    '    Try
    '        Dim studentNummer As String = ""
    '        While rd.Read
    '            Dim opl As New cOpleiding
    '            studentNummer = dbOsiris.oraSafeGetDecimal(rd, "studentnummer")
    '            opl.Code = dbOsiris.oraSafeGetString(rd, "Opleidingscode")
    '            opl.Naam = dbOsiris.oraSafeGetString(rd, "Opleidingsnaam")
    '            opl.Startdatum = dbOsiris.oraGetSafeDate(rd, "Startdatum")
    '            opl.Einddatum = dbOsiris.oraGetSafeDate(rd, "Einddatum")
    '            opl.Einddatum_werkelijk = dbOsiris.oraGetSafeDate(rd, "Einddatum_werkelijk")
    '            opl.TeamCode = dbOsiris.oraSafeGetString(rd, "Teamcodes")
    '            opl.TeamNaam = dbOsiris.oraSafeGetString(rd, "Teamnamen")

    '            'aanvullende velden bij de student toevoegen
    '            If dictOsirisStudenten.ContainsKey(studentNummer) Then
    '                'opleidingen bij de student toevoegen
    '                If Not dictOsirisStudenten(studentNummer).Opleidingen.ContainsKey(opl.UniqueKey) Then
    '                    dictOsirisStudenten(studentNummer).Opleidingen.Add(opl.UniqueKey, opl)
    '                End If
    '                'teams toevoegen
    '                dictOsirisStudenten(studentNummer).AddTeam(opl.TeamNaam, opl.TeamNaam)

    '            Else
    '                l.LOGTHIS("student niet gevonden voor opleiding: " & studentNummer)
    '            End If

    '            'teams toevoegen bij de student zelf

    '        End While
    '    Catch ex As Exception
    '        Return False

    '    End Try
    '    Return True
    'End Function
    'Private Function GetStudentKlassen() As Boolean
    '    l.LOGTHIS("Klassen bij de studenten opvragen en invullen")
    '    Dim rd As OracleDataReader = dbOsiris.oracleQueryUitvoeren(i.SqlStudentenKlasLesgroepen)
    '    If rd.HasRows = False Then
    '        rd.Close()
    '        l.LOGTHIS("Geen relatie student - klas - lesgroep beschikbaar")
    '        Return False
    '    End If

    '    Try
    '        Dim studentNummer As String = ""
    '        While rd.Read
    '            Dim deelname As New cGroepsDeelname
    '            studentNummer = dbOsiris.oraSafeGetDecimal(rd, "studentnummer")
    '            deelname.GroepsCode = dbOsiris.oraSafeGetString(rd, "groepscode")
    '            deelname.GroepNaam = dbOsiris.oraSafeGetString(rd, "groepsnaam")
    '            deelname.IngangsDatum = dbOsiris.oraGetSafeDate(rd, "ingangsdatum")
    '            deelname.AfloopDatum = dbOsiris.oraGetSafeDate(rd, "afloopdatum")


    '            'alle velden binnen bij de student de opleiding toevoegen
    '            If dictOsirisStudenten.ContainsKey(studentNummer) Then
    '                If Not dictOsirisStudenten(studentNummer).GroepsDeelnames.ContainsKey(deelname.UniqueKey) Then
    '                    dictOsirisStudenten(studentNummer).GroepsDeelnames.Add(deelname.UniqueKey, deelname)
    '                End If
    '            Else
    '                ' l.LOGTHIS("student niet gevonden voor groepsdeelname: " & studentNummer)
    '            End If
    '        End While
    '    Catch ex As Exception
    '        Return False

    '    End Try
    '    Return True
    'End Function
End Class
