import { useMemo } from 'react';

type PaginationResult = {
  totalPages: number;
  visiblePages: number[];
  startIndex: number;
  endIndex: number;
};

const MAX_PAGES_VISIBLE = 5;

export const usePagination = (
  totalCount: number,
  page: number,
  pageSize: number
): PaginationResult => {
  const totalPages = Math.max(1, Math.ceil(totalCount / pageSize));

  const visiblePages = useMemo(() => {
    const half = Math.floor(MAX_PAGES_VISIBLE / 2);
    let start = Math.max(1, page - half);
    let end = Math.min(totalPages, start + MAX_PAGES_VISIBLE - 1);
    start = Math.max(1, end - MAX_PAGES_VISIBLE + 1);

    return Array.from({ length: end - start + 1 }, (_, idx) => start + idx);
  }, [page, totalPages]);

  const startIndex = totalCount === 0 ? 0 : (page - 1) * pageSize + 1;
  const endIndex = Math.min(page * pageSize, totalCount);

  return { totalPages, visiblePages, startIndex, endIndex };
};
