type Props = {
  page: number;
  totalPages: number;
  visiblePages: number[];
  onPageChange: (nextPage: number) => void;
};

const Pagination = ({ page, totalPages, visiblePages, onPageChange }: Props) => {
  const handlePageChange = (nextPage: number) => {
    if (nextPage >= 1 && nextPage <= totalPages && nextPage !== page) {
      onPageChange(nextPage);
    }
  };

  return (
    <nav aria-label="Навигация по страницам справочника">
      <ul className="pagination pagination-sm mb-0">
        <li className={`page-item ${page === 1 ? 'disabled' : ''}`}>
          <button
            className="page-link"
            onClick={() => handlePageChange(page - 1)}
            type="button"
          >
            Назад
          </button>
        </li>
        {visiblePages.map((pageNumber) => (
          <li
            key={pageNumber}
            className={`page-item ${pageNumber === page ? 'active' : ''}`}
          >
            <button
              className="page-link"
              onClick={() => handlePageChange(pageNumber)}
              type="button"
            >
              {pageNumber}
            </button>
          </li>
        ))}
        <li className={`page-item ${page === totalPages ? 'disabled' : ''}`}>
          <button
            className="page-link"
            onClick={() => handlePageChange(page + 1)}
            type="button"
          >
            Вперёд
          </button>
        </li>
      </ul>
    </nav>
  );
};

export default Pagination;
