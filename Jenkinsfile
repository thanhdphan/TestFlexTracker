pipeline {

    	agent {
        	node {
            		label 'master'
        	}
    	}

 	stages {
           stage('Checkout') {
              steps   {
                 git 'https://github.com/thanhdphan/TestFlexTracker.git'
	      }
           }

          stage('Build') {
            steps {
                   bat '"C:\\Program Files\\Microsoft Visual Studio\\2022\\Professional\\MSBuild\\Current\\Bin\\MSBuild.exe" /t:Rebuild /p:Configuration=Debug TestFlexTracker.sln'
            }
          }
	
	stage('Execute') {
            steps {
                // Change to the directory containing the executable and run it
                bat '''
                    cd "C:\\Users\\phanth\\AppData\\Local\\Jenkins\\.jenkins\\workspace\\TestFlexTracker\\bin\\Debug\\net8.0"
                    TestFlexTracker.exe
                '''
            }
        }
     }
}
