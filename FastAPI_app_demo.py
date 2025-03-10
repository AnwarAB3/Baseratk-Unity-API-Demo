## import framework
from fastapi import FastAPI

## CORS must be imported to allow unity WebGL session to communicate with API
from fastapi.middleware.cors import CORSMiddleware

## Used for data validation but I don't understand it fully
from pydantic import BaseModel

## Initialize app
app = FastAPI()

## Defines strucutre of the data to be expected in the request
class Item(BaseModel):
    
    ## This holds the user's guess comming from the unity WebGL instance
    guess: str

# Configure CORS
app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],  # Allow all origins or specify your Unity WebGL URL
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

## Number to guess
number = "40"

## defines the route to accept POST requests from 
@app.post("/api/check_guess")

## Returns reponse in JSON after checking guess
async def check_guess(item: Item):
    if item.guess == number:
        return {"message": "Correct! You guessed the right number."}
    else:
        return {"message": "Incorrect! Try again."}

if __name__ == "__main__":

    ## Necessary to create local server instance???
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)