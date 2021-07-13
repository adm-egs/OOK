Module modAlgemeen
    Public l As cLogging
    Public ini As cIniFile
    Public i As cInstellingen
    Public dURLS As New Dictionary(Of String, String)
    Public dPersonen As New Dictionary(Of Long, cPersoon)
    Public dbOsiris As cDbUtils
    Public dbMiddleWare As cDbUtils
    Public dictOsirisStudentenKeyStudentNr As New Dictionary(Of String, cStudent)
    Public dAlleGroepen As New Dictionary(Of String, cGroep)

End Module
