import React from "react";
import {createRoot} from "react-dom/client";
import Home from "./Home";

const container = document.getElementById("root-react-app");
const root = createRoot(container);
const cart = JSON.parse(window.localStorage.getItem('oam_cake_cart') || null) || {};
root.render(<Home bcakes={window.bcakes} savedCart={cart}/>);

