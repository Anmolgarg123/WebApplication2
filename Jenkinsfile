pipeline {
    agent any

    environment {
        DOTNET_PATH = '"C:\\Program Files\\dotnet\\dotnet.exe"'
        PATH = "C:\\Program Files\\nodejs;C:\\Users\\samar\\.dotnet\\tools;${env.PATH}"
    }

    stages {
        stage('Checkout SCM') {
            steps {
                checkout scm
            }
        }

        stage('Debug Environment') {
            steps {
                echo "Checking NodeJS and .NET versions..."
                bat 'node -v'
                bat 'npm -v'
                bat '"C:\\Program Files\\dotnet\\dotnet.exe" --version'
            }
        }

        stage('Debug Workspace') {
            steps {
                echo "Listing workspace structure..."
                bat 'dir /s /b'
            }
        }

        stage('Build .NET API') {
    steps {
        bat '"C:\\Program Files\\dotnet\\dotnet.exe" build "C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2\\WebApplication2.sln" -c Release'
    }
}


        stage('Run .NET Tests') {
            steps {
                dir("${WORKSPACE}\\WebApplication2\\WebApplication2.Tests") {
                    bat '"C:\\Program Files\\dotnet\\dotnet.exe" test WebApplication2.Tests.csproj --logger "trx;LogFileName=TestResults.trx" -l "console;verbosity=detailed"'
                }
            }
        }

        stage('Convert TRX to JUnit') {
            steps {
                dir("${WORKSPACE}\\WebApplication2\\WebApplication2.Tests\\TestResults") {
                    bat '"C:\\Users\\samar\\.dotnet\\tools\\trx2junit.exe" TestResults.trx'
                }
            }
        }

        stage('Publish Test Results') {
            steps {
                junit "${WORKSPACE}\\WebApplication2\\WebApplication2.Tests\\TestResults\\TestResults.xml"
            }
        }

        stage('Code Quality') {
            steps {
                echo 'Running code quality checks...'
                dir("${WORKSPACE}\\WebApplication2\\WebApplication2") {
                    bat '"C:\\Program Files\\dotnet\\dotnet.exe" tool restore --verbosity minimal'
                    bat '"C:\\Program Files\\dotnet\\dotnet.exe" tool run dotnet-format WebApplication2.sln'
                }
            }
        }

        stage('Build Angular UI') {
            steps {
                dir("C:\\Users\\samar\\source\\repos\\webapp-ui") {
                    bat 'npm install'
                    bat 'npm run build'
                }
            }
        }

        stage('Copy Angular UI to API') {
            steps {
                bat '''
                robocopy "C:\\Users\\samar\\source\\repos\\webapp-ui\\dist\\webapp-ui" 
                "C:\\Users\\samar\\source\\repos\\Anmolgarg123\\WebApplication2\\WebApplication2\\wwwroot" 
                /E /NFL /NDL /NJH /NJS /nc /ns /np || exit 0
                '''
            }
        }

        stage('Deploy') {
            steps {
                echo 'Add deployment steps here (e.g., IIS, Azure, Docker)'
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
            echo 'Pipeline failed. Check logs for errors.'
        }
    }
}
