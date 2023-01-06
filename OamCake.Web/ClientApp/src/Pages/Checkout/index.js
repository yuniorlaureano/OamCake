import React from "react";
import {createRoot} from "react-dom/client";
import Checkout from "./Checkout";

const container = document.getElementById("root-react-app");
const root = createRoot(container);
root.render(<Checkout />);

