pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                git url: 'https://github.com/Anmolgarg123/WebApplication2.git', branch: 'main'
            }
        }

        stage('Build') {
            steps {
                bat '"C:\\Program Files\\dotnet\\dotnet.exe" build WebApplication2.sln -c Release'
            }
        }

        stage('Test') {
            steps {
                // Run tests and generate trx file
                bat '"C:\\Program Files\\dotnet\\dotnet.exe" test WebApplication2.Tests\\WebApplication2.Tests.csproj --logger "trx;LogFileName=TestResults.trx"'
            }
        }

        stage('Convert TRX to JUnit') {
            steps {
                // Convert trx -> JUnit XML
                bat '"C:\\Users\\hp\\.dotnet\\tools\\trx2junit.exe" WebApplication2.Tests\\TestResults\\TestResults.trx'
            }
        }

        stage('Publish Test Results') {
            steps {
                // Publish JUnit XML to Jenkins
                junit 'WebApplication2.Tests\\TestResults\\TestResults.xml'
            }
        }

        stage('Code Quality') {
            steps {
                echo 'Add SonarQube or CodeClimate steps here if needed'
            }
        }

        stage('Deploy') {
            steps {
                echo 'Add deployment steps here'
            }
        }
    }

    post {
        always {
            echo 'Pipeline finished!'
        }
        failure {
            echo 'Pipeline failed! Check logs.'
        }
    }
}
