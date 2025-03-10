## imports
from flask import Flask, request, jsonify

## CORS must be used to allow the unity WebGL session to communicate with the API
from flask_cors import CORS

## Initialize app
app = Flask(__name__)

## Enable CORS for all routes
CORS(app)

## number to guess
number = "50"

## defines the route to accept POST requests from
@app.route('/api/check_guess', methods=['POST'])

## handles incoming requests
def check_guess():

    ## Get JSON data from the request
    data = request.json

    ## Extract the 'guess' field
    guess = data.get('guess')

    ## returns response in JSON after checking guess
    if guess == number:
        return jsonify({"message": "Correct! You guessed the right number."}), 200
    else:
        return jsonify({"message": "Incorrect! Try again."}), 200

## Creates the local server instance??
if __name__ == '__main__':
    app.run(host='0.0.0.0', port=8000, debug=True)