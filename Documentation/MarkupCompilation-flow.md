Example of a typical build flow involving _PresentationBuildTasks.dll_ `Targets` and `Tasks`: 

```
PrepareResources
  MarkupCompilePass1
    MarkupCompilePass1 (Task)
		Language = C#
		ApplicationMarkup
			App.xaml
				Generator = MSBuild:Compile
		LanguageSourceExtension = .cs
		PageMarkup
			Dictionary1.xaml
				Generator = MSBuild:Compile
				SubType = Designer
			MainWindow.xaml
				Generator = MSBuild:Compile
		AssemblyName = wpf
		OutputType = WinExe
		References = [...]
		RootNamespace = wpf
		KnownReferencePaths = C:\Program Files\dotnet\sdk\3.1.101
		AlwaysCompileMarkupFilesInSeparateDomain = True
		HostInBrowser = false
		LocalizationDirectivesToLocFile = None
		SourceCodeFiles
			App.xaml.cs
			AssemblyInfo.cs
			MainWindow.xaml.cs
		DefineConstants = TRACE;DEBUG;NETCOREAPP;NETCOREAPP3_1
		XamlDebuggingInformation = True
		OutputPath = obj\Debug\netcoreapp3.1\
		OutputProperties:
			_RequireMCPass2ForMainAssembly = True
			_RequireMCPass2ForSatelliteAssemblyOnly = False
		OutputItems:
			Compile
				C:\temp\wpf\obj\Debug\netcoreapp3.1\MainWindow.g.cs
				C:\temp\wpf\obj\Debug\netcoreapp3.1\App.g.cs
			FileWrites
				C:\temp\wpf\obj\Debug\netcoreapp3.1\MainWindow.g.cs
				C:\temp\wpf\obj\Debug\netcoreapp3.1\App.g.cs
				C:\temp\wpf\obj\Debug\netcoreapp3.1\wpf_MarkupCompile.cache
				C:\temp\wpf\obj\Debug\netcoreapp3.1\wpf_MarkupCompile.lref
			_GeneratedCodeFiles
				C:\temp\wpf\obj\Debug\netcoreapp3.1\MainWindow.g.cs
				C:\temp\wpf\obj\Debug\netcoreapp3.1\App.g.cs

	
	MarkupCompilePass2ForMainAssembly
		GenerateTemporaryTargetAssembly
			GenerateTemporaryTargetAssembly (Task)
				CurrentProject = C:\temp\wpf\wpf.csproj
				MSBuildBinPath = C:\Program Files\dotnet\sdk\3.1.101
				ReferencePathTypeName = ReferencePath
				CompileTypeName = Compile
				GeneratedCodeFiles
					C:\temp\wpf\obj\Debug\netcoreapp3.1\MainWindow.g.cs
					C:\temp\wpf\obj\Debug\netcoreapp3.1\App.g.cs
				ReferencePath = [...]
				IntermediateOutputPath = obj\Debug\netcoreapp3.1\
				AssemblyName = wpf
				CompileTargetName = _CompileTemporaryAssembly
				
		MarkupCompilePass2
			MarkupCompilePass2 (Task)
				AssemblyName = wpf
				OutputType = WinExe
				Language = C#
				LocalizationDirectivesToLocFile = None
				RootNamespace = wpf
				References = [...]
				KnownReferencePaths = C:\Program Files\dotnet\sdk\3.1.101
				AlwaysCompileMarkupFilesInSeparateDomain = True
				XamlDebuggingInformation = True
				OutputPath = obj\Debug\netcoreapp3.1\	
				OutputItems: 
					GeneratedBaml
						C:\temp\wpf\obj\Debug\netcoreapp3.1\Dictionary1.baml
						C:\temp\wpf\obj\Debug\netcoreapp3.1\MainWindow.baml
					FileWrites				
						C:\temp\wpf\obj\Debug\netcoreapp3.1\Dictionary1.baml
						C:\temp\wpf\obj\Debug\netcoreapp3.1\MainWindow.baml
	
	FileClassification
		FileClassifier (Task)
			SourceFiles
				C:\temp\wpf\obj\Debug\netcoreapp3.1\Dictionary1.baml
				C:\temp\wpf\obj\Debug\netcoreapp3.1\MainWindow.baml
				Image1.bmp
			OutputType = WinExe
			OutputItems:
				MainEmbeddedFiles
					C:\temp\wpf\obj\Debug\netcoreapp3.1\Dictionary1.baml
					C:\temp\wpf\obj\Debug\netcoreapp3.1\MainWindow.baml
					Image1.bmp
					
	MainResourcesGeneration
		ResourcesGenerator (Task)
			ResourceFiles
				C:\temp\wpf\obj\Debug\netcoreapp3.1\Dictionary1.baml
				C:\temp\wpf\obj\Debug\netcoreapp3.1\MainWindow.baml
				Image1.bmp
			OutputPath = obj\Debug\netcoreapp3.1\
			OutputResourcesFile = obj\Debug\netcoreapp3.1\wpf.g.resources
			OutputItems:
				FileWrites = obj\Debug\netcoreapp3.1\wpf.g.resources

```