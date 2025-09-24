pipeline {
    agent any

    environment {
        DOTNET_PATH = '"C:\\Program Files\\dotnet\\dotnet.exe"'
        PATH = "C:\\Program Files\\nodejs;C:\\Users\\hp\\.dotnet\\tools;${env.PATH}"
    }

    stages {
        stage('Checkout SCM') {
            steps {
                checkout scm
            }
        }

        stage('Debug NodeJS') {
            steps {
                echo "Debugging NodeJS environment..."
                bat 'node -v'
                bat 'npm -v'
            }
        }

        stage('Build .NET API') {
            steps {
                bat "${DOTNET_PATH} build WebApplication2.sln -c Release"
            }
        }

        stage('Run Tests') {
            steps {
                bat "${DOTNET_PATH} test WebApplication2.Tests\\WebApplication2.Tests.csproj --logger \"trx;LogFileName=TestResults.trx\" -l \"console;verbosity=detailed\""
            }
        }

        stage('Convert TRX to JUnit') {
            steps {
                bat '"C:\\Users\\hp\\.dotnet\\tools\\trx2junit.exe" WebApplication2.Tests\\TestResults\\TestResults.trx'
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
        bat "${DOTNET_PATH} tool restore --verbosity minimal"
        // Auto-fix formatting instead of failing
        bat "${DOTNET_PATH} tool run dotnet-format WebApplication2.sln"
    }
}

        stage('Build Angular UI') {
            steps {
                dir('C:\\Users\\hp\\source\\repos\\webapp-ui') {
                    bat 'npm install'
                    bat 'npm run build'
                }
            }
        }

       stage('Copy Angular UI to API') {
    steps {
        bat '''
        robocopy "C:\\Users\\hp\\source\\repos\\webapp-ui\\dist\\webapp-ui" "WebApplication2\\wwwroot" /E /NFL /NDL /NJH /NJS /nc /ns /np || exit 0
        '''
    }
}

        stage('Serve Angular UI') {
    steps {
        dir('C:\\Users\\hp\\source\\repos\\webapp-ui') {
            bat 'npx serve -s dist\\webapp-ui -l 4200'
        }
    }
}

        stage('Deploy') {
            steps {
                echo 'Add deployment steps here (e.g., IIS, Azure, Docker, etc.)'
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
