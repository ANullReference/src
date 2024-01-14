import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { GenerateRandomGrid } from "./components/GenerateRandomGrid";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  },
  {
    path: "/generate-random-grid"
    ,element: <GenerateRandomGrid />
  }
];

export default AppRoutes;
