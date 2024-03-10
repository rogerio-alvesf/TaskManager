#!/bin/bash

# Function to fill the configuration-variables.env file based on the email provider
fill_env_based_on_provider() {
    local email_provider="$1"
    case "$email_provider" in
        "gmail" )
            smtp_server="smtp.gmail.com"
            smtp_port="587"
            ;;
        "outlook" )
            smtp_server="smtp.office365.com"
            smtp_port="587"
            ;;
        "yahoo" )
            smtp_server="smtp.mail.yahoo.com"
            smtp_port="587"
            ;;
        "icloud" )
            smtp_server="smtp.mail.me.com"
            smtp_port="587"
            ;;
        "aol" )
            smtp_server="smtp.aol.com"
            smtp_port="587"
            ;;
        # Add other email providers here as needed
        * ) 
            echo "The email provider '$email_provider' is not supported."
            exit 1
            ;;
    esac

    # Fill in the missing information based on the provider
    sed -i '/^SMTP_SERVER=/d' configuration-variables.env
    sed -i '/^SMTP_PORT=/d' configuration-variables.env
    sed -i '/^SMTP_USERNAME=/d' configuration-variables.env
    sed -i '/^SMTP_PASSWORD=/d' configuration-variables.env

    echo "SMTP_SERVER=$smtp_server" >> configuration-variables.env
    echo "SMTP_PORT=$smtp_port" >> configuration-variables.env
}

# Function to validate email address format
validate_email() {
    local email="$1"
    if [[ ! "$email" =~ ^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$ ]]; then
        echo "Invalid email address. Please provide a valid email address."
        return 1
    fi
}

# Main function
main() {
    echo "Welcome to the configuration-variables.env setup script."
    echo "Would you like to configure SMTP credentials?"
    echo "1 - for yes"
    echo "2 - for no"

    # Loop until a valid choice is entered
    while true; do
        read -p "Enter your choice: " configure_smtp

        case "$configure_smtp" in
            1 ) 
                read -p "Enter your email address: " email_address
                validate_email "$email_address"
                if [ $? -ne 0 ]; then
                    continue
                fi
                # Extract the email provider from the provided email address
                email_provider=$(echo "$email_address" | cut -d '@' -f 2 | cut -d '.' -f 1)
                fill_env_based_on_provider "$email_provider"
                read -s -p "Enter your password: " smtp_password
                echo # Add a new line after password input

                echo "SMTP_USERNAME=$email_address" >> configuration-variables.env
                echo "SMTP_PASSWORD=$smtp_password" >> configuration-variables.env
                echo "The information has been saved to the configuration-variables.env file."

                # Wait for 5 seconds before closing the prompt
                sleep 3
                break # Exit the loop
                ;;
            2 ) 
                read -n 1 -s -r -p "Press Enter to close the prompt..."
                echo # Add a new line after pressing Enter
                break # Exit the loop
                ;;
            * ) 
                echo "Invalid choice. Please enter '1' for yes or '2' for no."
                ;;
        esac
    done
}

main