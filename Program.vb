Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports System.Xml

Module Program
    Sub Main()
        ' Initialize the Chrome WebDriver
        Dim driver As IWebDriver = New ChromeDriver()
        Dim testResult As Boolean = True ' Variable to track test result
        Dim testName As String = "Check Image Presence"
        Dim imageName As String = "OpenText%20Login%20Sandbox2.png"
        Dim resultMessage As String = ""

        Try
            ' Navigate to the desired webpage
            driver.Navigate().GoToUrl("http://saascloudflextr.swinfra.net:9000")

            ' Check for the image by src attribute
            Dim image As IWebElement = Nothing

            Try
                image = driver.FindElement(By.XPath($"//img[contains(@src, '{imageName}')]"))
                resultMessage = "PASS: The image with the name '" & imageName & "' is present on the page."
                Console.WriteLine(resultMessage)
            Catch ex As NoSuchElementException
                resultMessage = "FAIL: The image with the name '" & imageName & "' is NOT found on the page."
                Console.WriteLine(resultMessage)
                testResult = False ' Update test result to fail
            End Try

            ' If the image is found, print additional details
            If image IsNot Nothing Then
                Console.WriteLine("Image Source: " & image.GetAttribute("src"))
            End If

        Catch ex As Exception
            resultMessage = "FAIL: An error occurred: " & ex.Message
            Console.WriteLine(resultMessage)
            testResult = False ' Update test result to fail
        Finally
            ' Close the browser
            driver.Quit()
        End Try

        ' Report the final test result
        If testResult Then
            Console.WriteLine("Test Result: PASS")
        Else
            Console.WriteLine("Test Result: FAIL")
        End If

        ' Export results to XML
        ExportResultsToXml(testName, testResult, resultMessage)
    End Sub

    Sub ExportResultsToXml(testName As String, testResult As Boolean, resultMessage As String)
        Dim xmlDoc As New XmlDocument()
        Dim root As XmlElement = xmlDoc.CreateElement("TestResults")
        xmlDoc.AppendChild(root)

        Dim testNode As XmlElement = xmlDoc.CreateElement("Test")
        root.AppendChild(testNode)

        Dim nameNode As XmlElement = xmlDoc.CreateElement("Name")
        nameNode.InnerText = testName
        testNode.AppendChild(nameNode)

        Dim resultNode As XmlElement = xmlDoc.CreateElement("Result")
        resultNode.InnerText = If(testResult, "PASS", "FAIL")
        testNode.AppendChild(resultNode)

        Dim messageNode As XmlElement = xmlDoc.CreateElement("Message")
        messageNode.InnerText = resultMessage
        testNode.AppendChild(messageNode)

        ' Save the XML document to a file
        Dim xmlFilePath As String = "TestResults.xml"
        xmlDoc.Save(xmlFilePath)

        Console.WriteLine($"Test results exported to {xmlFilePath}")
    End Sub
End Module
