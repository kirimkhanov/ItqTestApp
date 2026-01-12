import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { ReferenceItemsList } from "../components";
import {
  fetchReferenceItems,
  setPage,
  setPageSize,
  setSearch,
} from "../features/referenceItems/referenceItemsSlice";
import type { AppDispatch, RootState } from "../store";
import { useNavigate } from "react-router-dom";

const ReferenceItemsListPage = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch<AppDispatch>();
  const { items, totalCount, page, pageSize, search, status, error } = useSelector(
    (state: RootState) => state.referenceItems
  );

  useEffect(() => {
    dispatch(fetchReferenceItems({ page, pageSize, search }));
  }, [dispatch, page, pageSize, search]);

  const handleAdd = () => navigate("/add");

  const handlePageChange = (nextPage: number) => {
    dispatch(setPage(nextPage));
  };

  const handlePageSizeChange = (nextPageSize: number) => {
    dispatch(setPageSize(nextPageSize));
  };

  const handleSearchChange = (nextSearch: string) => {
    dispatch(setSearch(nextSearch));
  };

  return (
    <ReferenceItemsList
      items={items}
      totalCount={totalCount}
      page={page}
      pageSize={pageSize}
      search={search}
      status={status}
      error={error}
      onAdd={handleAdd}
      onPageChange={handlePageChange}
      onPageSizeChange={handlePageSizeChange}
      onSearchChange={handleSearchChange}
    />
  );
};

export default ReferenceItemsListPage;
