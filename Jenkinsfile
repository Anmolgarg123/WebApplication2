pipeline {
    agent any

    environment {
        DOTNET = '"C:\\Program Files\\dotnet\\dotnet.exe"'
        TEST_RESULTS = 'TestResults.trx'
        JUNIT_RESULTS = 'TestResults.xml'
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/Anmolgarg123/WebApplication2.git'
            }
        }

        stage('Build') {
            steps {
                bat "${env.DOTNET} build WebApplication2.sln -c Release"
            }
        }

        stage('Test') {
            steps {
                // Run tests and output TRX file
                bat "${env.DOTNET} test WebApplication2.Tests\\WebApplication2.Tests.csproj --logger trx;LogFileName=${env.TEST_RESULTS}"

                // Convert TRX to JUnit XML for Jenkins
                bat "trx2junit ${env.TEST_RESULTS}"

                // Publish JUnit results
                junit "${env.JUNIT_RESULTS}"
            }
        }

        stage('Code Quality') {
            steps {
                echo "Code Quality stage: configure SonarQube or other tools here"
            }
        }

        stage('Deploy') {
            steps {
                echo "Deploy stage: configure Docker, AWS, or Azure deployment here"
            }
        }

        stage('Monitoring') {
            steps {
                echo "Monitoring stage: configure Prometheus, New Relic, or Datadog here"
            }
        }
    }

    post {
        always {
            echo "Pipeline finished"
        }
        failure {
            echo "Pipeline failed. Check logs!"
        }
        success {
            echo "Pipeline succeeded!"
        }
    }
}
