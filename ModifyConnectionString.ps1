Param($config, $key, $value)
$doc = New-Object System.Xml.XmlDocument
$doc.Load($config)
$node = $doc.SelectSingleNode('configuration/connectionStrings/add[@name="' + $key + '"]')
$node.Attributes['connectionString'].Value = $value
$doc.Save($config)