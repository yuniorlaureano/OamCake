import React from "react";
import {createRoot} from "react-dom/client";
import Projection from "./Projection";

const container = document.getElementById("root-react-app");
const root = createRoot(container);
root.render(<Projection/>);

