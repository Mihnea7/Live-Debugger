pipeline {
    agent any

    stages {
        stage ("Build") {
            steps
            {
                echo "Starting Build stage"
                sh "dotnet build sample_project"
            }
            post {
                failure {
                    echo "Build failed"
                }
                success {
                    echo "Build succeded"
                }
            }
        }

        stage ("Test") {
            steps {
                echo "Starting Test stage"
                sh "dotnet test sample_project"
            }

            post {
                failure {
                    echo "Test failed"
                }
                success {
                    echo "Test succeded"
                }
            }
        }

    }
}