import { useNavigate } from "react-router-dom";
import ReferenceItemsAddForm from "../components/ReferenceItemsAddForm";

const ReferenceItemsAddPage = () => {
  const navigate = useNavigate();
  
  const handleAddBack = () => navigate("/");
  const handleAddSaved = () => navigate("/");

  return (
    <ReferenceItemsAddForm onBack={handleAddBack} onSaved={handleAddSaved} />
  );
};

export default ReferenceItemsAddPage;
