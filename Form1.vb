Imports Efarmogh.StatTypes
Imports System.IO
Imports System.Text.RegularExpressions

Public Class Form1

    Dim whereisAge As Integer
    Dim whereisSex As Integer


    Dim types As New StatTypes
    Dim k As Integer = 0
    Dim memmory1(100) As String


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        TextBox1.Text = OpenFileDialog1.FileName
    End Sub



    Dim ArrayValues As List(Of String()) = New List(Of String())
    Dim collunm1() As Integer
    Dim collunm2() As Integer
    Dim collumn3() As Integer
    Dim collumn4() As Integer

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        If String.IsNullOrEmpty(TextBox1.Text) Then
            MessageBox.Show("Please Select A  File ")
        End If

        Dim sr As New StreamReader(TextBox1.Text)

        Dim scores() As String
        Dim score As String
        Dim scores1() As String
        Dim score1 As String



        Dim counter As Integer = 0

        Do Until sr.Peek = -1
            score = sr.ReadLine()
            score = Regex.Replace(score, "\s", " ")
            scores = score.Split(" ")
            ArrayValues.Add(scores)
            If counter = 0 Then
                score1 = sr.ReadLine()
                score1 = Regex.Replace(score1, "\s", " ")
                scores1 = score1.Split(" ")
                counter = counter + 1
            End If
        Loop

        For points = 0 To ArrayValues(0).Length - 1
            Dim MyButton As New Button
            Dim point As New Point(0, (0 + points * 35))
            Dim size As New Size(120, 30)

            If ((ArrayValues(0)(points)).Equals("AGE")) Then
                whereisAge = points
            ElseIf ((ArrayValues(0)(points)).Equals("SEX")) Then
                whereisSex = points
            End If

            With MyButton
                .Size = size
                .Name = "myautobutton" + points.ToString()
                .Location = point
                .Text = ArrayValues(0)(points)
            End With
            AddHandler MyButton.Click, AddressOf MyTextbox_Changed
            Panel1.Controls.Add(MyButton)

        Next points









        ArrayValues.RemoveRange(0, 1)






    End Sub

    Public Function GetCollunm(ByVal test As List(Of String()), ByVal collumn As Integer) As Integer()







        Dim strArr(test.Count - 1) As Integer
        ' test.RemoveRange(0, 1)
        '  Dim temp As Double
        Dim i As Integer = 0
        For Each valuearr As String() In test
            '   temp = Convert.ToInt32(valuearr(collumn))
            Try
                strArr(i) = Convert.ToInt32(valuearr(collumn))
            Catch ex As Exception
                MessageBox.Show("Collumn contains strings")

                Exit Function
            End Try



            i = i + 1
        Next

        Return strArr

    End Function





    Friend Sub MyTextbox_Changed(sender As Object, e As EventArgs)
        'Dim nameofchart As String = (DirectCast(sender, Button)).Text

        Dim num1 As String
        num1 = (DirectCast(sender, Button)).Name.Replace("myautobutton", "")
        Dim num As Integer = Convert.ToInt32(num1)
        collunm1 = GetCollunm(ArrayValues, num)
        If (collunm1 Is Nothing) Then
            Exit Sub
        End If



        Dim meanmaths As Double = types.meanType(collunm1)
        Dim variancemaths As Double = types.varianceType(collunm1, meanmaths)
        Dim sdmaths As Double = types.standarDeviationType(variancemaths)


        Dim kurtosisMaths As Double = types.kurtosisType(collunm1, meanmaths, sdmaths)
        Dim crMaths As Double = types.criticalRatioType(collunm1, meanmaths, sdmaths)
        Dim skewMaths As Double = types.skewnessType(collunm1, meanmaths, sdmaths)
        ListBox2.Items.Clear()
        ListBox2.Items.Add(DirectCast(sender, Button).Text.ToString() + " : " +
            Math.Round(kurtosisMaths, 2).ToString() & "                     " & Math.Round(crMaths, 2).ToString() & "                 " & Math.Round(skewMaths, 2).ToString())


        Dim collunmNum As List(Of Integer) = New List(Of Integer)
        Dim collunmSum As List(Of Integer) = New List(Of Integer)
        Dim collunmName As List(Of Integer) = New List(Of Integer)

        Dim collunmAll As List(Of ClassType) = New List(Of ClassType)
        Dim collunmTemp() As Integer
        Dim tempdouble As Double


        If (num1 <> whereisAge And num1 <> whereisSex) Then

            collunmTemp = GetCollunm(ArrayValues, whereisAge)

            Dim i As Integer = 0
            For Each valuearr As Integer In collunmTemp
                '   temp = Convert.ToInt32(valuearr(collumn))
                If (collunmName.Contains(valuearr)) Then

                    Dim tempexist As Integer = collunmName.IndexOf(valuearr)
                    collunmSum(tempexist) += collunm1(i)
                    collunmNum(tempexist) += 1

                    collunmAll(tempexist).sum += collunm1(i)
                    collunmAll(tempexist).num += 1
                    collunmAll(tempexist).allvalues.Add(collunm1(i))

                Else

                    collunmName.Add(valuearr)
                    collunmSum.Add(collunm1(i))
                    collunmNum.Add(1)

                    collunmAll.Add(New ClassType(valuearr, collunm1(i)))

                End If

                i = i + 1
            Next
        End If


        Chart1.Series(0).Points.Clear()
        ListBox1.Items.Clear()
        collunmAll.Sort()

        Dim divtemp As Double = 0
        Dim results As String = ""
        Dim results1 As String = ""
        If (collunmName.Count > 0) Then

            Dim i As Integer = 0

            'For Each valuearr As Integer In collunmName
            'tempdouble = (collunmSum(i) / collunmNum(i))
            'divtemp = types.varianceType(collunm1, tempdouble)
            'results += "(" + valuearr.ToString + " , " + tempdouble.ToString + ")" '+ " , " + divtemp.ToString + ")"
            'results += "(" + collunmAll(i).num.ToString + " , " + collunmAll(i).getMeanType().ToString + " , " + divtemp.ToString + ") \n"
            'Me.Chart1.Series("Series1").Points.AddXY(valuearr, tempdouble)


            'i = i + 1
            ' Next
            ' ListBox1.Items.Add(results)
            'i = 0
            'results = ""


            For Each valuearr As Integer In collunmName
                tempdouble = (collunmSum(i) / collunmNum(i))

                'results += "(" + valuearr.ToString + " , " + tempdouble.ToString + " , " + divtemp.ToString + ") \n"
                results += "( Age " + collunmAll(i).name.ToString + " ,  Mean " + collunmAll(i).getMeanType().ToString + ") "

                results1 += "( Age " + collunmAll(i).name.ToString + " ,  SD " + collunmAll(i).standarDeviationType().ToString + ") "

                Me.Chart1.Series("Series1").Points.AddXY(valuearr, tempdouble)


                i = i + 1
            Next
            ListBox1.Items.Add(results)
            ListBox1.Items.Add(results1)
        End If




        memmory1(k) = num1


        ListBox3.Items.Clear()


        If k > 0 Then
            Dim m As Integer = k - 1
            Dim value1 As String = memmory1(k)
            Dim value2 As String = memmory1(m)

            Dim number1 As Integer = Convert.ToInt32(value1)
            collumn3 = GetCollunm(ArrayValues, value1)

            Dim number2 As Integer = Convert.ToInt32(value2)
            collumn4 = GetCollunm(ArrayValues, value2)

            If (collumn3 Is Nothing) Then
                Exit Sub
            End If
            If (collumn4 Is Nothing) Then
                Exit Sub
            End If





            Dim meanNewColl As Double = types.meanType(collumn3)
            Dim meanNewColl2 As Double = types.meanType(collumn4)

            Dim varianceNewColl As Double = types.varianceType(collumn3, meanNewColl)
            Dim varianceNewColl2 As Double = types.varianceType(collumn4, meanNewColl2)

            Dim standarDevi As Double = types.standarDeviationType(varianceNewColl)
            Dim standarDevi2 As Double = types.standarDeviationType(varianceNewColl2)



            Dim correlation As Double = types.correlationType(collumn3, collumn4, meanNewColl, meanNewColl2)
            Dim ciCorrellation As Double = types.findingCICorrelation(standarDevi, standarDevi2, correlation, meanNewColl, meanNewColl2, collumn3, collumn4)


            ListBox3.Items.Add("Correlation   " + correlation.ToString)
            ListBox3.Items.Add("Correlation with Ci   " + ciCorrellation.ToString)





        End If

        k = k + 1
    End Sub

End Class
