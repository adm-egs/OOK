Module modOudeCode
    'Try
    '    Dim rd As OracleDataReader = dbOsiris.oracleQueryUitvoeren(SqlStudentMutaties(Last_check_Date("last_check_student")))
    '    If Not IsNothing(rd) Then
    '        If rd.HasRows Then
    '            'aantal rows opvragen
    '            Dim rdCount As OracleDataReader = dbOsiris.oracleQueryUitvoeren(SqlCountStudentMutaties(Last_check_Date("last_check_student")))
    '            rdCount.Read()
    '            Dim max As Long = dbOsiris.oraSafeGetDecimal(rdCount, "aantal")
    '            Dim Counter As Long = 0
    '            frmMain.pbMutaties.Maximum = max
    '            frmMain.pbMutaties.Value = Counter
    '            frmMain.tsAutoVerwerkenActie.Text = max & " studentmutaties"

    '            While rd.Read
    '                If i.Stoppen = True Then Exit Function
    '                sStudentNummer = dbOsiris.oraSafeGetDecimal(rd, "studentnummer")
    '                mutatieDatum = dbOsiris.oraGetSafeDate(rd, "mutatie_datum")
    '                If mutatieDatum > OudsteMutatieDatumStudent Then
    '                    OudsteMutatieDatumStudent = mutatieDatum   'opslaan wat de eerste mutatiedatum is voor toekomstige mutaties
    '                End If

    '                GetStudentsOsiris(sStudentNummer)
    '                dictOsirisStudentenKeyStudentNr(sStudentNummer).ChangeUserInOO()
    '                l.LOGTHIS("Studentmutatie verwerkt: " & sStudentNummer, 10)
    '                Counter += 1
    '                frmMain.pbMutaties.Value = Counter
    '                If (Counter Mod 10 = 0) Then
    '                    'verwerkte mutatiedatum opslaan
    '                    LastMutatieDatumCheckedStudent = CheckDate(OudsteMutatieDatumStudent)
    '                    Save_check_date("last_check_student", LastMutatieDatumCheckedStudent)
    '                    'ini.WriteString("Check", "last_check_student", LastMutatieDatumCheckedStudent.ToString("yyyy-MM-dd HH:mm:ss"))

    '                End If
    '                Application.DoEvents()
    '                frmMain.tsAutoVerwerkenActie.Text = Counter & "/" & max & " studentmutaties"
    '                Application.DoEvents()
    '            End While
    '        End If
    '    End If


    'Catch ex As Exception
    '    l.LOGTHIS("Fout bij verwerken studentmutatie: " & ex.Message)
    'End Try
    'LastMutatieDatumCheckedStudent = CheckDate(OudsteMutatieDatumStudent)
    ''ini.WriteString("Check", "last_check_student", LastMutatieDatumCheckedStudent.ToString("yyyy-MM-dd HH:mm:ss"))
    'Save_check_date("last_check_student", LastMutatieDatumCheckedStudent)


    'Try
    '    Dim rd As OracleDataReader = dbOsiris.oracleQueryUitvoeren(SqlGroepMutaties(Last_check_Date("last_check_groep")))
    '    Dim rdCount As OracleDataReader = dbOsiris.oracleQueryUitvoeren(sqlCountGroepsMutaties(Last_check_Date("last_check_groep")))

    '    If Not IsNothing(rd) Then
    '        If rd.HasRows Then
    '            If i.Stoppen = True Then Exit Function
    '            'aantal opvragen
    '            rdCount.Read()
    '            Dim max As Long = dbOsiris.oraSafeGetDecimal(rdCount, "aantal")
    '            Dim Counter As Long = 0
    '            frmMain.pbMutaties.Maximum = max
    '            frmMain.pbMutaties.Value = Counter
    '            frmMain.tsCurrentState.Text = "Working"
    '            frmMain.tsAutoVerwerkenActie.Text = "Groepsmutaties " & max
    '            frmMain.pbMutaties.Visible = True

    '            While rd.Read
    '                sStudentNummer = dbOsiris.oraSafeGetDecimal(rd, "studentnummer")
    '                mutatieDatum = dbOsiris.oraGetSafeDate(rd, "mutatie_datum")
    '                If mutatieDatum > OudsteMutatieDatumGroep Then
    '                    OudsteMutatieDatumGroep = mutatieDatum   'opslaan wat de eerste mutatiedatum is voor toekomstige mutaties
    '                End If

    '                If GetStudentsOsiris(sStudentNummer) = True Then
    '                    dictOsirisStudentenKeyStudentNr(sStudentNummer).ChangeUserInOO()
    '                    l.LOGTHIS("Groepsmutatie verwerkt voor student : " & sStudentNummer, 10)
    '                End If

    '                Counter += 1
    '                If (Counter Mod 10 = 0) Then
    '                    'verwerkte mutatiedatum opslaan
    '                    LastMutatieDatumCheckedGroep = CheckDate(OudsteMutatieDatumGroep)
    '                    ini.WriteString("Check", "last_check_groep", LastMutatieDatumCheckedGroep.ToString("yyyy-MM-dd HH:mm:ss"))
    '                End If

    '                Application.DoEvents()
    '                frmMain.tsAutoVerwerkenActie.Text = Counter & "/" & max & " groepsmutaties"
    '                frmMain.pbMutaties.Value = Counter

    '                Application.DoEvents()
    '            End While
    '            frmMain.pbMutaties.Visible = False
    '        End If
    '    End If
    'Catch ex As Exception
    '    l.LOGTHIS("Fout bij verwerken groepsmutatie: " & ex.Message, 50)
    'End Try
    'LastMutatieDatumCheckedGroep = CheckDate(OudsteMutatieDatumGroep)
    'ini.WriteString("Check", "last_check_groep", LastMutatieDatumCheckedGroep.ToString("yyyy-MM-dd HH:mm:ss"))



    ''opleidingsmutaties verwerken
    'Dim EersteMutatieDatumOpleiding As Date = New Date(2000, 8, 1)
    'frmMain.tsCurrentState.Text = "Checking opleidingen mutaties"
    'Try
    '    Dim rd As OracleDataReader = dbOsiris.oracleQueryUitvoeren(SqlOpleidingMutaties(Last_check_Date("last_check_opleiding")))
    '    If Not IsNothing(rd) Then
    '        If rd.HasRows Then
    '            Dim rdCOunt As OracleDataReader = rd
    '            Dim lCount As Long = 0
    '            While rdCOunt.Read
    '                lCount += 1
    '            End While
    '            Dim max As Long = lCount
    '            Dim Counter As Long = 0
    '            frmMain.pbMutaties.Maximum = max
    '            frmMain.pbMutaties.Value = Counter
    '            frmMain.tsCurrentState.Text = "Working"
    '            frmMain.tsAutoVerwerkenActie.Text = "Opleidings mutaties " & max
    '            frmMain.pbMutaties.Visible = True

    '            While rd.Read
    '                If i.Stoppen = True Then
    '                    ini.WriteString("Check", "last_check_opleiding", EersteMutatieDatumOpleiding.ToString("yyyy-MM-dd HH:mm:ss"))
    '                    Exit Function
    '                End If
    '                Counter += 1
    '                sStudentNummer = dbOsiris.oraSafeGetDecimal(rd, "studentnummer")
    '                mutatieDatum = dbOsiris.oraGetSafeDate(rd, "mutatie_datum")
    '                mutatieDatum = New Date(mutatieDatum.Year, mutatieDatum.Month, mutatieDatum.Day, mutatieDatum.Hour, mutatieDatum.Minute, mutatieDatum.Second)
    '                If mutatieDatum > EersteMutatieDatumOpleiding Then
    '                    EersteMutatieDatumOpleiding = mutatieDatum   'opslaan wat de eerste mutatiedatum is voor toekomstige mutaties
    '                End If

    '                If GetStudentsOsiris(sStudentNummer) = True Then
    '                    dictOsirisStudentenKeyStudentNr(sStudentNummer).ChangeUserInOO()
    '                    l.LOGTHIS("Opleidingsmutatie verwerkt voor student : " & sStudentNummer, 10)
    '                End If
    '                If (Counter Mod 10 = 0) Then
    '                    'verwerkte mutatiedatum opslaan
    '                    'EersteMutatieDatumOpleiding = CheckDate(OudsteMutatieDatum)
    '                    ini.WriteString("Check", "last_check_opleiding", EersteMutatieDatumOpleiding.ToString("yyyy-MM-dd HH:mm:ss"))
    '                End If

    '                Application.DoEvents()
    '                frmMain.tsAutoVerwerkenActie.Text = Counter & "/" & max & " opleidingsmutaties"
    '                frmMain.pbMutaties.Value = Counter
    '                Application.DoEvents()
    '            End While
    '        End If
    '    End If
    'Catch ex As Exception
    '    l.LOGTHIS("Fout bij verwerken OpleidingsMutatie: " & ex.Message, 50)
    'End Try
End Module
