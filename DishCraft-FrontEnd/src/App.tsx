import {useEffect, useState} from 'react'
import './App.css'
import axios from 'axios'
import {Header, List} from "semantic-ui-react";

function App() {
  const [ingredients, setIngredients] = useState([]);
  const [measurements, setMeasurements] = useState([]);
  const [recipes, setRecipes] = useState([]);


    useEffect(() => {
    axios.get('http://localhost:5000/api/ingredients')
        .then(response => {
          setIngredients(response.data);
        })
  }, [])

    useEffect(() => {
        axios.get('http://localhost:5000/api/measurements')
            .then(response => {
                setMeasurements(response.data);
            })
    }, [])

    useEffect(() => {
        axios.get('http://localhost:5000/api/recipes')
            .then(response => {
                setRecipes(response.data);
            })
    }, [])

  return (
      <div><Header as='h2' icon='users' content='Ingredients'/>
        <List>
          {ingredients.map((ingredient: any) => (
              <List.Item key={ingredient.id}>
                {ingredient.name}: {ingredient.ingredientCategory}
              </List.Item>
          ))}
        </List>
        <List>
          {recipes.map((recipe: any) => (
              <List.Item key={recipe.id}>
                  {recipe.name}: {recipe.recipeCategory}
              </List.Item>
          ))}
        </List>
      </div>
  )
}

export default App
