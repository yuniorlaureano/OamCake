import React from "react";
import {createRoot} from "react-dom/client";
import Ingredient from "./Ingredient";

const container = document.getElementById("root-react-app");
const root = createRoot(container);
root.render(<Ingredient/>);

