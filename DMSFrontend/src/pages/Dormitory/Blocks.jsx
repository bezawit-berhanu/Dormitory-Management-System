import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import {
    getBlocks,
    deactivateBlock,
} from "../../services/dormitoryService";
import "../../App.css";

const Blocks = () => {
    const [blocks, setBlocks] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [searchTerm, setSearchTerm] = useState("");
    const [deactivatingId, setDeactivatingId] = useState(null);

    useEffect(() => {
        let cancelled = false;

        const loadBlocks = async () => {
            try {
                const data = await getBlocks();

                if (!cancelled) {
                    setBlocks(data);
                }
            } catch {
                if (!cancelled) {
                    setError("Unable to load block records.");
                }
            } finally {
                if (!cancelled) {
                    setLoading(false);
                }
            }
        };

        loadBlocks();

        return () => {
            cancelled = true;
        };
    }, []);

    const handleDeactivate = async (id, name) => {
        const confirmed = window.confirm(
            `Are you sure you want to deactivate "${name}"?`
        );

        if (!confirmed) {
            return;
        }

        try {
            setError("");
            setDeactivatingId(id);

            await deactivateBlock(id);

            setBlocks((currentBlocks) =>
                currentBlocks.map((block) =>
                    block.blockId === id
                        ? { ...block, isActive: false }
                        : block
                )
            );
        } catch {
            setError("Unable to deactivate the block.");
        } finally {
            setDeactivatingId(null);
        }
    };

    const filteredBlocks = blocks.filter((block) =>
        `${block.blockName} ${block.description}`
            .toLowerCase()
            .includes(searchTerm.toLowerCase())
    );

    if (loading) {
        return (
            <div className="page-container">
                <div className="loading-state">
                    Loading block records...
                </div>
            </div>
        );
    }

    return (
        <div className="page-container">
            <div className="page-header">
                <div>
                    <h1>Blocks</h1>
                    <p>
                        Manage blocks within the dormitory structures.
                    </p>
                </div>

                <div className="action-buttons">
                    <Link
                        to="/dormitories"
                        className="secondary-button"
                    >
                        ← Back to Dormitories
                    </Link>

                    <Link
                        to="/blocks/create"
                        className="primary-button"
                    >
                        + Add Block
                    </Link>

                    <Link
                        to="/floors"
                        className="secondary-button"
                    >
                        View Floors →
                    </Link>
                </div>
            </div>

            <div className="content-card">
                {error && (
                    <div className="error-state">
                        {error}
                    </div>
                )}

                <div className="table-toolbar">
                    <div>
                        <h2>Block Records</h2>

                        <span className="record-count">
                            {blocks.length} record
                            {blocks.length !== 1 ? "s" : ""}
                        </span>
                    </div>

                    <input
                        type="text"
                        className="search-input"
                        placeholder="Search by block name or description..."
                        value={searchTerm}
                        onChange={(event) =>
                            setSearchTerm(event.target.value)
                        }
                    />
                </div>

                {filteredBlocks.length === 0 ? (
                    <div className="empty-state">
                        <h3>No blocks found</h3>

                        <p>
                            {searchTerm
                                ? "Try a different search term."
                                : "There are no block records yet."}
                        </p>
                    </div>
                ) : (
                    <div className="table-wrapper">
                        <table className="data-table">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Block Name</th>
                                    <th>Dormitory ID</th>
                                    <th>Description</th>
                                    <th>Status</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>

                            <tbody>
                                {filteredBlocks.map((block) => (
                                    <tr key={block.blockId}>
                                        <td>{block.blockId}</td>

                                        <td className="name-cell">
                                            {block.blockName}
                                        </td>

                                        <td>{block.dormitoryId}</td>

                                        <td>
                                            {block.description}
                                        </td>

                                        <td>
                                            {block.isActive !== false ? (
                                                <span className="status-active">
                                                    Active
                                                </span>
                                            ) : (
                                                <span className="status-inactive">
                                                    Inactive
                                                </span>
                                            )}
                                        </td>

                                        <td>
                                            <div className="action-buttons">
                                                {block.isActive !== false && (
                                                    <>
                                                        <Link
                                                            to={`/blocks/edit/${block.blockId}`}
                                                            className="edit-button"
                                                        >
                                                            Edit
                                                        </Link>

                                                        <button
                                                            type="button"
                                                            className="deactivate-button"
                                                            onClick={() =>
                                                                handleDeactivate(
                                                                    block.blockId,
                                                                    block.blockName
                                                                )
                                                            }
                                                            disabled={
                                                                deactivatingId ===
                                                                block.blockId
                                                            }
                                                        >
                                                            {deactivatingId ===
                                                                block.blockId
                                                                ? "Deactivating..."
                                                                : "Deactivate"}
                                                        </button>
                                                    </>
                                                )}

                                                {block.isActive === false && (
                                                    <span className="inactive-label">
                                                        Deactivated
                                                    </span>
                                                )}
                                            </div>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                )}
            </div>
        </div>
    );
};

export default Blocks;