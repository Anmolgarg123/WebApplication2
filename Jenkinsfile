pipeline {
    agent any

    environment {
        DOTNET = '"C:\\Program Files\\dotnet\\dotnet.exe"'
        TRX2JUNIT = '"C:\\Users\\hp\\.dotnet\\tools\\trx2junit.exe"'
        ANGULAR_PROJECT = 'C:\\Users\\hp\\source\\repos\\webapp-ui'
        ANGULAR_APP_NAME = 'webapp-ui' // Usually same as project folder name in dist/
    }

    stages {
        stage('Checkout SCM') {
            steps {
                checkout scm
            }
        }

        stage('Build .NET API') {
            steps {
                bat "${DOTNET} build WebApplication2.sln -c Release"
            }
        }

        stage('Run Tests') {
            steps {
                bat "${DOTNET} test WebApplication2.Tests\\WebApplication2.Tests.csproj --logger \"trx;LogFileName=TestResults.trx\" -l \"console;verbosity=detailed\""
            }
        }

        stage('Convert TRX to JUnit') {
            steps {
                bat "${TRX2JUNIT} WebApplication2.Tests\\TestResults\\TestResults.trx"
            }
        }

        stage('Publish Test Results') {
            steps {
                junit 'WebApplication2.Tests\\TestResults\\TestResults.xml'
            }
        }

        stage('Code Quality') {
            steps {
                echo 'Running code quality checks...'
                bat "${DOTNET} tool restore --verbosity minimal"
                bat script: "${DOTNET} tool run dotnet-format WebApplication2.sln --verify-no-changes", returnStatus: true
            }
        }

        stage('Build Angular UI') {
            steps {
                bat "cd ${ANGULAR_PROJECT} && npm install"
                bat "cd ${ANGULAR_PROJECT} && ng build --prod"
            }
        }

        stage('Copy Angular to API') {
            steps {
                // Copies Angular dist files to API's wwwroot folder
                bat "xcopy /E /Y /I ${ANGULAR_PROJECT}\\dist\\${ANGULAR_APP_NAME} WebApplication2\\wwwroot"
            }
        }

        stage('Deploy') {
            steps {
                echo 'Add deployment steps here (e.g., publish API or copy to server)'
            }
        }
    }

    post {
        always {
            echo 'Pipeline finished!'
        }
        success {
            echo 'Pipeline completed successfully!'
        }
        failure {
            echo 'Pipeline failed. Check the logs for errors.'
        }
    }
}
