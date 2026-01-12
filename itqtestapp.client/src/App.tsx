import './App.css';
import { Navigate, Route, Routes } from 'react-router-dom';
import ReferenceItemsLayout from './components/ReferenceItemsLayout';
import ReferenceItemsAddPage from './pages/ReferenceItemsAddPage';
import ReferenceItemsListPage from './pages/ReferenceItemsListPage';

const App = () => {
  return (
    <Routes>
      <Route element={<ReferenceItemsLayout />}>
        <Route index element={<ReferenceItemsListPage />} />
        <Route
          path="add"
          element={<ReferenceItemsAddPage />}
        />
        <Route path="*" element={<Navigate to="/" replace />} />
      </Route>
    </Routes>
  );
};

export default App;
