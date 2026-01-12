import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import ReferenceItemsList from "../components/ReferenceItemsList";
import {
  fetchReferenceItems,
  setPage,
  setPageSize,
} from "../features/referenceItems/referenceItemsSlice";
import type { AppDispatch, RootState } from "../store";
import { useNavigate } from "react-router-dom";

const ReferenceItemsListPage = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch<AppDispatch>();
  const { items, totalCount, page, pageSize, status, error } = useSelector(
    (state: RootState) => state.referenceItems
  );

  useEffect(() => {
    dispatch(fetchReferenceItems({ page, pageSize }));
  }, [dispatch, page, pageSize]);

  const handleAdd = () => navigate("/add");

  const handlePageChange = (nextPage: number) => {
    dispatch(setPage(nextPage));
  };

  const handlePageSizeChange = (nextPageSize: number) => {
    dispatch(setPageSize(nextPageSize));
  };

  return (
    <ReferenceItemsList
      items={items}
      totalCount={totalCount}
      page={page}
      pageSize={pageSize}
      status={status}
      error={error}
      onAdd={handleAdd}
      onPageChange={handlePageChange}
      onPageSizeChange={handlePageSizeChange}
    />
  );
};

export default ReferenceItemsListPage;
