type Props = {
  value: number;
  onChange: (value: number) => void;
};

const ReferenceItemsPageSizeSelect = ({ value, onChange }: Props) => (
  <div className="d-flex align-items-center gap-2">
    <label className="small text-muted" htmlFor="pageSizeSelect">
      Строк на странице
    </label>
    <select
      id="pageSizeSelect"
      className="form-select form-select-sm w-auto"
      value={value}
      onChange={(event) => onChange(Number(event.target.value))}
    >
      <option value={5}>5</option>
      <option value={10}>10</option>
      <option value={20}>20</option>
      <option value={50}>50</option>
    </select>
  </div>
);

export default ReferenceItemsPageSizeSelect;
