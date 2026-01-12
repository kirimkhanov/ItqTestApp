type Props = {
  value: string;
  onChange: (value: string) => void;
};

const ReferenceItemsSearch = ({ value, onChange }: Props) => (
  <div className="d-flex align-items-center gap-2">
    <label className="small text-muted" htmlFor="referenceItemsSearch">
      Поиск
    </label>
    <input
      id="referenceItemsSearch"
      className="form-control form-control-sm"
      type="search"
      value={value}
      onChange={(event) => onChange(event.target.value)}
      placeholder="Поиск по коду или значению"
    />
  </div>
);

export default ReferenceItemsSearch;
